using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIController
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseUIController : MonoBehaviour
    {
        private UIDocument _document;
        protected VisualElement Root { get; private set; }

        private VisualElement FirstFocusedElement { get; set; }

        protected virtual void Awake()
        {
            _document = GetComponent<UIDocument>();
            Root = _document.rootVisualElement;
            
            Close();
        }
        
        public void Open()
        {
            FocusFirst();
            if (Root != null)
            {
                Root.style.display = DisplayStyle.Flex;
            }
        }
        
        public void Close()
        {
            if (Root != null)
            {
                Root.style.display = DisplayStyle.None;
            }
        }

        private void FocusFirst()
        {
            FirstFocusedElement?.Focus();
        }

        protected void FocusFirst(VisualElement element)
        {
            FirstFocusedElement = element;
            FocusFirst();
        }

    }
}