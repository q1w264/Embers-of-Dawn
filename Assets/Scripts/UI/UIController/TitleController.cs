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
        //TODO UIBehavior 完成: ButtonChangeGameStateBehavior.
        
        [Header("Level Manager")]
        [SerializeField] private LevelManager levelManager;

        private readonly ButtonExitGameBehavior _buttonExitGameBehavior = new();
        private ButtonChangGameSceneBehavior _startButtonChangGameSceneBehavior;
        private ButtonOpenUIBehavior  _settingsButtonOpenUIBehavior;

        private void Start()
        {
            _startButton = Root.Q<Button>("start-button");
            _settingsButton = Root.Q<Button>("settings-button");
            _exitButton = Root.Q<Button>("exit-button");
            FocusFirst(_startButton);

            _startButtonChangGameSceneBehavior = new ButtonChangGameSceneBehavior(levelManager, "Game");
            _settingsButtonOpenUIBehavior = new ButtonOpenUIBehavior(UIManager, "Settings");
            
            _buttonExitGameBehavior.Bind(_exitButton);
            _startButtonChangGameSceneBehavior.Bind(_startButton);
            _settingsButtonOpenUIBehavior.Bind(_settingsButton);
        }

        protected override void OnDestroy()
        {
            _buttonExitGameBehavior.Unbind(_exitButton);
            _startButtonChangGameSceneBehavior.Unbind(_startButton);
            _settingsButtonOpenUIBehavior.Unbind(_settingsButton);
        }
    }
}