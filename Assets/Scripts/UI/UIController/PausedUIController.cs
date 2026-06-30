using Core;
using Scene;
using UI.UIBehavior.ChangeGameSceneBehavior;
using UI.UIBehavior.ChangeGameStateBehavior;
using UI.UIBehavior.OpenUIBehavior;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIController
{
    /// <summary>
    /// Controller for pause menu actions (resume, settings, quit).
    /// </summary>
    public class PausedUIController : SoundedUIController
    {
        private Button _backButton;
        private Button _settingsButton;
        private Button _quitButton;
        
        [Header("Level Manager")] [SerializeField]
        private LevelManager levelManager;
        
        private readonly ButtonChangeGameStateBehavior _changePlayingStateBehavior = new(GameState.Playing);
        private readonly ButtonChangeGameStateBehavior _changeTitleStateBehavior = new(GameState.Title);
        private ButtonOpenUIBehavior _openSettingsBehavior;
        private ButtonChangeGameSceneBehavior _goToTitleBehavior;
        
        private void Start()
        {
            _backButton = Root.Q<Button>("paused__back-button");
            _settingsButton = Root.Q<Button>("paused__settings-button");
            _quitButton = Root.Q<Button>("paused__quit-button");
            FocusFirst(_backButton);
            
            _openSettingsBehavior = new ButtonOpenUIBehavior(UIManager, "Settings");
            _goToTitleBehavior = new ButtonChangeGameSceneBehavior(levelManager, "Title");
            
            _changeTitleStateBehavior.Bind(_quitButton);
            _changePlayingStateBehavior.Bind(_backButton);
            _openSettingsBehavior.Bind(_settingsButton);
            _goToTitleBehavior.Bind(_quitButton);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_backButton != null)
            {
                _changePlayingStateBehavior.Unbind(_backButton);
            }

            if (_quitButton != null)
            {
                _changeTitleStateBehavior.Unbind(_quitButton);
                _goToTitleBehavior?.Unbind(_quitButton);
            }

            if (_settingsButton != null)
            {
                _openSettingsBehavior?.Unbind(_settingsButton);
            }
        }
    }
}