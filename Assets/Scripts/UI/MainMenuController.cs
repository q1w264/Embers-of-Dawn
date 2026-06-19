using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public PlayerInput playerInput;
        
        private void Start()
        {
            if (playerInput != null)
                OnMenu();
            var uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                return;
            }

            // 2. 获取根节点
            var root = uiDocument.rootVisualElement;
            if (root == null) return;
            
            var mainButton = root.Q<Button>("start-button");
            mainButton?.Focus();
            
        }

        private void OnMenu()
        {
            playerInput.SwitchCurrentActionMap("UI");
        }

        private void OnGameplay()
        {
            playerInput.SwitchCurrentActionMap("Gameplay");
        }
    }
}
