using UnityEngine.UIElements;

namespace UI.UIBehavior
{
    public abstract class BaseUIBehavior<TElement> : IUIBehavior<TElement> where TElement : VisualElement
    {
        public abstract void Bind(TElement element);

        public abstract void Unbind(TElement element);

        public virtual void Bind(VisualElement element)
        {
            var elements = element.Query<TElement>().ToList();
            foreach(var e in elements)
            {
                Bind(e);
            }
        }

        public virtual void Unbind(VisualElement element)
        {
            var elements = element.Query<TElement>().ToList();
            foreach(var e in elements)
            {
                Unbind(e);
            }
        }

        protected abstract EventCallback<TCtx> GetBehaviorHandler<TCtx>();
    }
}