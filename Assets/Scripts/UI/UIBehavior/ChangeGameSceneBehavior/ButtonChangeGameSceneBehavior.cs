using Scene;
using UnityEngine.UIElements;

namespace UI.UIBehavior.ChangeGameSceneBehavior
{
    /// <summary>
    /// Button behavior that requests a scene change from <see cref="LevelManager"/>.
    /// </summary>
    public sealed class ButtonChangeGameSceneBehavior : BaseButtonUIBehavior
    {
        private readonly LevelManager _levelManager;
        private readonly string _targetSceneName;
        
        /// <summary>
        /// Creates a behavior that changes to a target scene when triggered.
        /// </summary>
        /// <param name="levelManager">Level manager used to load scene.</param>
        /// <param name="targetSceneName">Target scene name.</param>
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