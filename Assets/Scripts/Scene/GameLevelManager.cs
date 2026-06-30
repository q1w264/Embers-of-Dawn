using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scene
{
    /// <summary>
    /// Gameplay scene manager that reacts to game state changes and swaps action maps.
    /// </summary>
    public class GameLevelManager : LevelManager
    {
        private PlayerInput _playerInput;
        protected override void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            if (_playerInput == null)
            {
                Debug.LogError("PlayerInput component not found on GameObject.");
            }

            GameHub.Get.OnGameStateChanged += OnGameStateChanged;
            base.Awake();
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Paused:
                    UIManager.Open("Paused");
                    SwitchActionMap("UI");
                    break;
                case GameState.Playing:
                    UIManager.CloseAll();
                    SwitchActionMap("Player");
                    //TODO UIManager.Open("GUI");
                    break;
                case GameState.Ended:
                    //TODO UIManager.Open("GameOver");
                    break;
                case GameState.Title:
                    SwitchActionMap("UI");
                    break;
                default:
                    Debug.LogError($"{gameState} is not a valid game state");
                    break;
            }
        }

        private void OnDisable()
        {
            UnsubscribeFromGameHub();
        }

        private void OnDestroy()
        {
            UnsubscribeFromGameHub();
        }

        private void UnsubscribeFromGameHub()
        {
            if (GameHub.TryGet(out var gameHub))
            {
                gameHub.OnGameStateChanged -= OnGameStateChanged;
            }
        }

        private void SwitchActionMap(string actionMapName)
        {
            if (_playerInput == null) return;
            _playerInput.SwitchCurrentActionMap(actionMapName);
        }
    }
}