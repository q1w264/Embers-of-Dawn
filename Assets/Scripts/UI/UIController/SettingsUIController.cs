using Input;

namespace UI.UIController
{
    /// <summary>
    /// Controller for settings panel, including cancel-to-close handling.
    /// </summary>
    public class SettingsUIController : SoundedUIController
    {
        private GameInputSystem  _gameInputSystem;
        private GameInputSystem.UIActions  _uiActions;

        protected override void Awake()
        {
            base.Awake();
            _gameInputSystem = new GameInputSystem();
            _uiActions = _gameInputSystem.UI;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _uiActions.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _uiActions.Disable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _gameInputSystem?.Dispose();
        }

        private void Update()
        {
            if (!IsOpen) return;
            if (_uiActions.Cancel.WasPressedThisFrame())
            {
                UIManager.CloseLast();
            }
        }
    }
}