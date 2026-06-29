using Core;
using UnityEngine.UIElements;

namespace UI.UIBehavior.OpenUIBehavior
{
    public sealed class ButtonOpenUIBehavior : BaseButtonUIBehavior
    {
        private readonly UIManager _uiManager;
        private readonly string _targetUIName;

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