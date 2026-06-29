using UnityEngine.UIElements;

namespace UI.UIBehavior
{
    public abstract class BaseButtonUIBehavior : BaseUIBehavior<Button>
    {
        protected EventCallback<ClickEvent> ClickEventHandler { get; set; }
        protected EventCallback<NavigationSubmitEvent> NavigationSubmitEventHandler { get; set; }

        public override void Bind(Button element)
        {
            element.RegisterCallback(ClickEventHandler);
            element.RegisterCallback(NavigationSubmitEventHandler);
        }

        public override void Unbind(Button element)
        {
            element.UnregisterCallback(ClickEventHandler);
            element.UnregisterCallback(NavigationSubmitEventHandler);
        }
        
        protected override EventCallback<TCtx> GetBehaviorHandler<TCtx>()
        {
            return _ => Behavior();
        }

        protected abstract void Behavior();
    }
}