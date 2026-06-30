using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gaming
{
    /// <summary>
    /// Translates player pause input into game state transitions.
    /// </summary>
    public class GameStateChanger : MonoBehaviour
    {
        private GameHub _gameHub;
        private PlayerInput _playerInput;
        private InputAction _pauseAction;
        private bool _isPauseSubscribed;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            if (_playerInput == null)
            {
                Debug.LogError("PlayerInput component not found on GameObject.");
            }

            _gameHub = GameHub.Get;
        }

        private void OnEnable()
        {
            SubscribePauseAction();
        }

        private void OnDisable()
        {
            UnsubscribePauseAction();
        }

        private void OnDestroy()
        {
            UnsubscribePauseAction();
        }

        private void OnPausePerformed(InputAction.CallbackContext ctx)
        {
            _gameHub?.ChangeGameState(GameState.Paused);
        }

        private void SubscribePauseAction()
        {
            if (_playerInput == null || _isPauseSubscribed) return;

            _pauseAction ??= _playerInput.actions["Pause"];
            if (_pauseAction == null)
            {
                Debug.LogError("Pause action not found on PlayerInput.");
                return;
            }

            _pauseAction.performed += OnPausePerformed;
            _isPauseSubscribed = true;
        }

        private void UnsubscribePauseAction()
        {
            if (!_isPauseSubscribed || _pauseAction == null) return;
            _pauseAction.performed -= OnPausePerformed;
            _isPauseSubscribed = false;
        }
    }
}