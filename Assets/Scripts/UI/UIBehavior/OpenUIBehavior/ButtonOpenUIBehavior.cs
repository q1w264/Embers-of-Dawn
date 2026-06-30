using Core;
using UnityEngine.UIElements;

namespace UI.UIBehavior.OpenUIBehavior
{
    /// <summary>
    /// Button behavior that opens another UI controller by name.
    /// </summary>
    public sealed class ButtonOpenUIBehavior : BaseButtonUIBehavior
    {
        private readonly UIManager _uiManager;
        private readonly string _targetUIName;

        /// <summary>
        /// Creates behavior that opens target UI when triggered.
        /// </summary>
        /// <param name="uiManager">UI manager that owns controller stack.</param>
        /// <param name="targetUIName">Target controller name.</param>
        public ButtonOpenUIBehavior(UIManager uiManager, string targetUIName)
        {
            _uiManager = uiManager;
            _targetUIName = targetUIName;
            ClickEventHandler = GetBehaviorHandler<ClickEvent>();
            NavigationSubmitEventHandler = GetBehaviorHandler<NavigationSubmitEvent>();
        }
        
        protected override void Behavior()
        {
            _uiManager.Open(_targetUIName);
        }
    }
}