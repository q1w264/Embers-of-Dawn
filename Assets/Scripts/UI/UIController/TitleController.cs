using UnityEngine.UIElements;
using Utility;

namespace UI.UIController
{
    public class TitleController : SoundedUIController
    {
        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;
        //TODO UIBehavior 完成: ButtonExitGameBehavior, ButtonChangeGameStateBehavior, ButtonChangeSceneBehavior.
        private void Start()
        {
            _startButton = Root.Q<Button>("start-button");
            _settingsButton = Root.Q<Button>("settings-button");
            _exitButton = Root.Q<Button>("exit-button");
            FocusFirst(_startButton);
            
            
        }
    }
}