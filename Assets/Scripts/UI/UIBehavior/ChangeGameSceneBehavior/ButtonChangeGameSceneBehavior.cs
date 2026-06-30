using Scene;
using UnityEngine.UIElements;

namespace UI.UIBehavior.ChangeGameSceneBehavior
{
    public sealed class ButtonChangeGameSceneBehavior : BaseButtonUIBehavior
    {
        private readonly LevelManager _levelManager;
        private readonly string _targetSceneName;
        
        public ButtonChangeGameSceneBehavior(LevelManager levelManager, string targetSceneName)
        {
            NavigationSubmitEventHandler = GetBehaviorHandler<NavigationSubmitEvent>();
            ClickEventHandler = GetBehaviorHandler<ClickEvent>();
            _levelManager = levelManager;
            _targetSceneName = targetSceneName;
        }
        protected override void Behavior()
        {
            _levelManager.GoToScene(_targetSceneName);
        }
    }
}