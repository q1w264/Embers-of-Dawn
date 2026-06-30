using Scene;
using UI.UIBehavior.ChangeGameSceneBehavior;
using UI.UIBehavior.ExitGameBehavior;
using UI.UIBehavior.OpenUIBehavior;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIController
{
    public class TitleController : SoundedUIController
    {
        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;

        [Header("Level Manager")] [SerializeField]
        private LevelManager levelManager;
        
        [Header("Game Scene")] [SerializeField]
        private string targetGameScene = "Game";

        private readonly ButtonExitGameBehavior _buttonExitGameBehavior = new();
        private ButtonChangeGameSceneBehavior _startButtonChangeGameSceneBehavior;
        private ButtonOpenUIBehavior _settingsButtonOpenUIBehavior;

        private void Start()
        {
            _startButton = Root.Q<Button>("start-button");
            _settingsButton = Root.Q<Button>("settings-button");
            _exitButton = Root.Q<Button>("exit-button");
            FocusFirst(_startButton);

            _startButtonChangeGameSceneBehavior = new ButtonChangeGameSceneBehavior(levelManager, targetGameScene);
            _settingsButtonOpenUIBehavior = new ButtonOpenUIBehavior(UIManager, "Settings");

            _buttonExitGameBehavior.Bind(_exitButton);
            _startButtonChangeGameSceneBehavior.Bind(_startButton);
            _settingsButtonOpenUIBehavior.Bind(_settingsButton);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_exitButton != null)
            {
                _buttonExitGameBehavior.Unbind(_exitButton);
            }

            if (_startButton != null)
            {
                _startButtonChangeGameSceneBehavior.Unbind(_startButton);
            }
            if (_settingsButton != null)
            {
                _settingsButtonOpenUIBehavior.Unbind(_settingsButton);
            }
        }
    }
}