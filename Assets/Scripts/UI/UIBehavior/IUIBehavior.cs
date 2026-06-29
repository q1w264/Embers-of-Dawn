using UnityEngine.UIElements;

namespace UI.UIBehavior
{
    public interface IUIBehavior<in TElement> where TElement : VisualElement
    {
        public void Bind(TElement element);
        
        public void Unbind(TElement element);

        public void Bind(VisualElement element);

        public void Unbind(VisualElement element);
    }
}