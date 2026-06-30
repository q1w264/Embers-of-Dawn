using System.Collections.Generic;
using UI.UIController;

namespace Core
{
    /// <summary>
    /// Manages UI controller lookup, stack navigation, and visibility switching.
    /// </summary>
    public class UIManager
    {
        private readonly Dictionary<string, BaseUIController> _uiDict = new();
        private readonly Stack<BaseUIController> _uiStack;

        /// <summary>
        /// Builds the manager dictionary and injects this manager into controllers.
        /// </summary>
        /// <param name="uiControllers">Controllers registered for the current scene.</param>
        public UIManager(List<BaseUIController> uiControllers)
        {
            _uiStack = new Stack<BaseUIController>();
            foreach (var controller in uiControllers)
            {
                if (controller!=null && controller.gameObject != null)
                {
                    _uiDict[controller.name] = controller;
                    controller.UIManager = this;
                }
            }
        }

        /// <summary>
        /// Opens a UI by name and pushes it to the controller stack.
        /// </summary>
        /// <param name="uiName">Controller name.</param>
        public void Open(string uiName)
        {
            // 1. 缓存命中：如果已经实例化过，直接显示并压栈
            if (!_uiDict.TryGetValue(uiName, out var controller)) return;
            PushAndOpenUI(controller);
        }
        
        /// <summary>
        /// Closes the latest opened UI and restores previous one if any.
        /// </summary>
        public void CloseLast()
        {
            if (_uiStack.Count <= 0) return;
            var controller = _uiStack.Pop();
            controller.Close();
            if (_uiStack.Count <= 0) return;
            _uiStack.Peek().Open();
        }

        /// <summary>
        /// Closes all currently opened UIs.
        /// </summary>
        public void CloseAll()
        {
            while (_uiStack.Count > 0)
            {
                var controller = _uiStack.Pop();
                controller.Close();
            }
        }

        private void PushAndOpenUI(BaseUIController controller)
        {
            if (_uiStack.Count > 0)
            {
                _uiStack.Peek().Close();
            }

            _uiStack.Push(controller);
            controller.Open();
        }
    }
}