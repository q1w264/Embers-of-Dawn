using System.Collections.Generic;
using UI.UIController;

namespace Core
{
    public class UIManager
    {
        private readonly Dictionary<string, BaseUIController> _uiDict = new();
        private readonly Stack<BaseUIController> _uiStack;

        public UIManager(List<BaseUIController> uiControllers)
        {
            _uiStack = new Stack<BaseUIController>();
            foreach (var controller in uiControllers)
            {
                if (controller.gameObject)
                {
                    _uiDict[controller.name] = controller;
                    controller.UIManager = this;
                }
            }
        }

        public void Open(string uiName)
        {
            // 1. 缓存命中：如果已经实例化过，直接显示并压栈
            if (!_uiDict.TryGetValue(uiName, out var controller)) return;
            PushAndOpenUI(controller);
        }
        
        public void CloseLast()
        {
            if (_uiStack.Count <= 0) return;
            var controller = _uiStack.Pop();
            controller.Close();
            if (_uiStack.Count <= 0) return;
            _uiStack.Peek().Open();
        }

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