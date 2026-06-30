using UnityEngine.UIElements;

namespace UI.UIBehavior
{
    /// <summary>
    /// Defines bind/unbind contract for UI Toolkit behavior components.
    /// </summary>
    /// <typeparam name="TElement">Target visual element type.</typeparam>
    public interface IUIBehavior<in TElement> where TElement : VisualElement
    {
        /// <summary>
        /// Binds behavior callbacks to one element.
        /// </summary>
        /// <param name="element">Target element.</param>
        public void Bind(TElement element);
        
        /// <summary>
        /// Removes behavior callbacks from one element.
        /// </summary>
        /// <param name="element">Target element.</param>
        public void Unbind(TElement element);

        /// <summary>
        /// Binds behavior callbacks to a visual tree.
        /// </summary>
        /// <param name="element">Tree root.</param>
        public void Bind(VisualElement element);

        /// <summary>
        /// Removes behavior callbacks from a visual tree.
        /// </summary>
        /// <param name="element">Tree root.</param>
        public void Unbind(VisualElement element);
    }
}