using UnityEngine.UIElements;
using UnityEngine;

namespace UI.UIBehavior
{
    /// <summary>
    /// Base behavior for button interaction through click and submit events.
    /// </summary>
    public abstract class BaseButtonUIBehavior : BaseUIBehavior<Button>
    {
        protected EventCallback<ClickEvent> ClickEventHandler { get; set; }
        protected EventCallback<NavigationSubmitEvent> NavigationSubmitEventHandler { get; set; }
        private int _lastBehaviorFrame = -1;

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
            return _ =>
            {
                var currentFrame = Time.frameCount;
                if (_lastBehaviorFrame == currentFrame) return;
                _lastBehaviorFrame = currentFrame;
                Behavior();
            };
        }

        protected abstract void Behavior();
    }
}