using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIController
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseUIController : MonoBehaviour
    {
        private UIDocument _document;
        private VisualElement Root { get; set; }

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            Root = _document.rootVisualElement;
            
            Close();
        }
        
        public virtual void Open()
        {
            if (Root != null)
            {
                Root.style.display = DisplayStyle.Flex;
            }
        }
        
        public virtual void Close()
        {
            if (Root != null)
            {
                Root.style.display = DisplayStyle.None;
            }
        }
    }
}