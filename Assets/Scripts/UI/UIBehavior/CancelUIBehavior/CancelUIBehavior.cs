using System;
using System.ComponentModel;
using Core;
using UnityEngine.UIElements;

namespace UI.UIBehavior.CancelUIBehavior
{
    [Obsolete("CancelUIBehavior is deprecated. Use GameInputSystem.UI.Cancel.WasPressedThisFrame instead.")]
    public sealed class CancelUIBehavior<TUIElement> : BaseUIBehavior<TUIElement> where TUIElement : VisualElement
    {
        private readonly UIManager _uiManager;
        private readonly EventCallback<NavigationCancelEvent> _eventCallback;

        public CancelUIBehavior(UIManager uiManager)
        {
            _uiManager = uiManager;
            _eventCallback = GetBehaviorHandler<NavigationCancelEvent>();
        }
        
        public override void Bind(TUIElement element)
        {
            element.RegisterCallback(_eventCallback);
        }

        public override void Unbind(TUIElement element)
        {
            element.UnregisterCallback(_eventCallback);
        }
        
        // ReSharper disable once UnusedMember.Local
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not used in CancelUIBehavior. Use Bind(TUIElement element) instead.")]
        private new void Bind(VisualElement element)
        {
            _ = element;
        }
        
        // ReSharper disable once UnusedMember.Local
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not used in CancelUIBehavior. Use Unbind(TUIElement element) instead.")]
        private new void Unbind(VisualElement element)
        {
            _ = element;
        }
        

        protected override EventCallback<TCtx> GetBehaviorHandler<TCtx>()
        {
            return _ =>
            {
                _uiManager.CloseLast();
            };
        }
    }
}