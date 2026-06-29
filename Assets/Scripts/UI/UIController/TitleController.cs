using Scene;
using UI.UIBehavior.ChangeGameSceneBehavior;
using UI.UIBehavior.ExitGameBehavior;
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

        private void Start()
        {
            _startButton = Root.Q<Button>("start-button");
            _settingsButton = Root.Q<Button>("settings-button");
            _exitButton = Root.Q<Button>("exit-button");
            FocusFirst(_startButton);

            _startButtonChangGameSceneBehavior = new ButtonChangGameSceneBehavior(levelManager, "Game");

            _buttonExitGameBehavior.Bind(_exitButton);
            _startButtonChangGameSceneBehavior.Bind(_startButton);
        }

        private void OnDestroy()
        {
            _buttonExitGameBehavior.Unbind(_exitButton);
            _startButtonChangGameSceneBehavior.Unbind(_startButton);
        }
    }
}