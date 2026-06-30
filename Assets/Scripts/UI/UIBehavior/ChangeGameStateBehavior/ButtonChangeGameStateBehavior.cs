using System;
using UnityEngine.UIElements;

namespace UI.UIBehavior.ChangeGameStateBehavior
{
    public sealed class ButtonChangeGameStateBehavior : BaseButtonUIBehavior
    {
        private readonly Action _action;

        public ButtonChangeGameStateBehavior(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);
            _action = action;
            NavigationSubmitEventHandler = GetBehaviorHandler<NavigationSubmitEvent>();
            ClickEventHandler = GetBehaviorHandler<ClickEvent>();
        }
        
        protected override void Behavior()
        {
            _action.Invoke();
        }
    }
}