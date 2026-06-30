using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utility;

namespace Core
{
    /// <summary>
    /// Central game state hub that persists across scenes.
    /// </summary>
    public class GameHub : PersistentSingleton<GameHub>
    {
        [FormerlySerializedAs("_gameState")] [SerializeField] private GameState gameState;

        private Action<GameState> _onGameStateChanged;

        /// <summary>
        /// Raised when game state changes.
        /// </summary>
        public event Action<GameState> OnGameStateChanged
        {
            add
            {
                if (_onGameStateChanged == null || !_onGameStateChanged.GetInvocationList().Contains(value))
                    _onGameStateChanged += value;
            }
            remove => _onGameStateChanged -= value;
        }
        
        /// <summary>
        /// Gets the singleton instance, creating one if missing.
        /// </summary>
        public static GameHub Get
        {
            get
            {
                if (Instance != null) return Instance;
                var target = new GameObject("[GameHub]");
                Instance = target.AddComponent<GameHub>();
                return Instance;
            }
        }

        /// <summary>
        /// Tries to get existing instance without creating a new one.
        /// </summary>
        /// <param name="gameHub">Current instance when available.</param>
        /// <returns><c>true</c> when an instance exists.</returns>
        public static bool TryGet(out GameHub gameHub)
        {
            gameHub = Instance;
            return gameHub != null;
        }

        protected override void Awake()
        {
            base.Awake();
            if (InputSystem.settings != null)
            {
                InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            }
        }

        /// <summary>
        /// Applies a new game state and propagates state side effects.
        /// </summary>
        /// <param name="newState">Target game state.</param>
        public void ChangeGameState(GameState newState)
        {
            if (gameState == newState) return;
            gameState = newState;
            Time.timeScale = newState switch
            {
                GameState.Paused or GameState.Ended => 0f,
                GameState.Title or GameState.Playing => 1f,
                _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
            };

            _onGameStateChanged?.Invoke(newState);
        }
    }

    /// <summary>
    /// High-level state machine for gameplay flow.
    /// </summary>
    public enum GameState
    {
        Title,
        Playing,
        Paused,
        Ended
    }
}