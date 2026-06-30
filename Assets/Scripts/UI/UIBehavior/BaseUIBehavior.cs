using UnityEngine.UIElements;

namespace UI.UIBehavior
{
    /// <summary>
    /// Base class for behaviors that can bind to typed elements or to element trees.
    /// </summary>
    /// <typeparam name="TElement">Target visual element type.</typeparam>
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