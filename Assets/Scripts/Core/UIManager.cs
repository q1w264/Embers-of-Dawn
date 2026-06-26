using System.Collections.Generic;
using UI.UIController;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core
{
    public class UIManager : BaseManager
    {
        private readonly Stack<BaseUIController> _uiStack = new();
        private readonly Dictionary<string, BaseUIController> _uiDict = new();
        private readonly Dictionary<string, GameObject> _gameObjects = new();

        public UIManager(GameHub gameHub, List<GameObject> uiList) : base(gameHub)
        {
            foreach (var ui in uiList)
            {
                var controller = ui.GetComponent<BaseUIController>();
                if (controller == null) continue;
                _uiDict.Add(ui.name, controller);
                _gameObjects.Add(ui.name, ui);
            }
        }

        public void Open(string uiName)
        {
            // 1. 缓存命中：如果已经实例化过，直接显示并压栈
            if (_gameObjects.TryGetValue(uiName, out var gameObject) && _uiDict.TryGetValue(uiName, out var controller))
            {
                gameObject.SetActive(true);
                PushAndOpenUI(controller);
                return;
            }

            // 2. 缓存未命中：通过 Addressable 动态加载
            Addressables.InstantiateAsync(uiName).Completed += handle =>
            {
                if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogWarning($"Failed to load UI '{uiName}' via Addressable.");
                    return;
                }

                var instantiatedObject = handle.Result;
                var newController = instantiatedObject.GetComponent<BaseUIController>();
        
                if (newController == null)
                {
                    Debug.LogWarning($"UI '{uiName}' does not have a BaseUIController component.");
                    // 如果加载了不带脚本的物体，记得销毁或处理，防止内存泄漏
                    Addressables.ReleaseInstance(instantiatedObject); 
                    return;
                }

                // 存入缓存，下次就能直接命中上面的“缓存命中”分支
                _gameObjects[uiName] = instantiatedObject;
                _uiDict[uiName] = newController;
        
                PushAndOpenUI(newController);
            };
        }

        public void CloseLast()
        {
            if (_uiStack.Count <= 0) return;
            var controller = _uiStack.Pop();
            controller.Close();
            controller.gameObject.SetActive(false);
            if (_uiStack.Count <= 0) return;
            _uiStack.Peek().Open();
            _uiStack.Peek().gameObject.SetActive(true);
        }

        public void CloseAll()
        {
            while (_uiStack.Count > 0)
            {
                var controller = _uiStack.Pop();
                controller.Close();
                controller.gameObject.SetActive(false);
            }
        }
        
        private void PushAndOpenUI(BaseUIController controller)
        {
            if (_uiStack.Count > 0)
            {
                _uiStack.Peek().Close();
                _uiStack.Peek().gameObject.SetActive(false);
            }
            _uiStack.Push(controller);
            controller.gameObject.SetActive(true);
            controller.Open();
        }
        
        /// <summary>
        /// 离开场景时调用，彻底销毁场景中的 UI 实例并释放 Addressables 资源
        /// </summary>
        public void CleanupOnLeaveScene()
        {
            // 清空导航栈
            _uiStack.Clear();

            // 遍历所有缓存的 GameObject 进行物理销毁
            foreach (var kvp in _gameObjects)
            {
                var uiGo = kvp.Value;
                if (uiGo == null) continue;
                // 优先使用 Addressable 释放，如果不是 Addressables 加载的（比如构造函数传进来的），则常规销毁
                if (!Addressables.ReleaseInstance(uiGo))
                {
                    Object.Destroy(uiGo);
                }
            }

            // 清空字典缓存，迎接下一个场景
            _gameObjects.Clear();
            _uiDict.Clear();
        }
    }
}