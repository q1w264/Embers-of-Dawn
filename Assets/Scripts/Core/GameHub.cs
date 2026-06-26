using System;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class GameHub : MonoBehaviour
    {
        private GameState _gameState;

        private Action<GameState> _onGameStateChanged;

        public event Action<GameState> OnGameStateChanged
        {
            add
            {
                if (_onGameStateChanged == null || !_onGameStateChanged.GetInvocationList().Contains(value))
                    _onGameStateChanged += value;
            }
            remove => _onGameStateChanged -= value;
        }

        private static GameHub Instance { get; set; }

        public static GameHub Get
        {
            get
            {
                if (Instance != null) return Instance;
                var target = new GameObject("[GameHub]");
                Instance = target.AddComponent<GameHub>();
                DontDestroyOnLoad(target);
                return Instance;
            }
        }

        public void ChangeGameState(GameState newState)
        {
            if (_gameState == newState) return;
            _gameState = newState;
            Time.timeScale = newState switch
            {
                GameState.Paused or GameState.Ended or GameState.Title => 0f,
                GameState.Playing => 1f,
                _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
            };

            _onGameStateChanged?.Invoke(newState);
        }
    }

    public enum GameState
    {
        Title,
        Playing,
        Paused,
        Ended
    }
}