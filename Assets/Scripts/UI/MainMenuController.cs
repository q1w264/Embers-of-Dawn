using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string startButtonTargetScene; // 通过 Inspector 关联主场景资源
        [SerializeField] private string settingButtonTargetScene;

        private void Start()
        {
            var uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                return;
            }

            // 2. 获取根节点
            var root = uiDocument.rootVisualElement;
            if (root == null) return;

            var mainButton = root.Q<Button>("start-button");
            var settingsButton = root.Q<Button>("settings-button");
            var exitButton = root.Q<Button>("exit-button");
            mainButton?.Focus();
            if (mainButton != null && startButtonTargetScene != "") mainButton.clicked += MainButtonClick;
            if (settingsButton != null && settingButtonTargetScene != "") settingsButton.clicked += SettingButtonClick;
            if (exitButton != null) exitButton.clicked += ExitGame;
        }

        private void LoadNextScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private void MainButtonClick()
        {
            LoadNextScene(startButtonTargetScene);
        }

        private void SettingButtonClick()
        {
            LoadNextScene(settingButtonTargetScene);
        }

        private void ExitGame()
        {
            // 如果当前是在 Unity 编辑器中运行
#if UNITY_EDITOR
            // 相当于手动点掉编辑器顶部的 "Play" 播放按钮
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // 如果是打包出来的正式独立游戏（Windows / Mac / Android 等）
            Application.Quit();
#endif
        }
    }
}