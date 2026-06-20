using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class DefaultUIEventListener : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip clickSound; // 比如 Kenney 的点击音效
        
        private void Awake()
        {
            // 1. 获取 UIDocument 组件
            var uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                return;
            }

            // 2. 获取根节点
            var root = uiDocument.rootVisualElement;
            if (root == null) return;

            // 3. 自动向下钻取，搜寻内部所有的 Button（会完美抓到你的 start-button 和 exit-button）
            var allButtons = root.Query<Button>().ToList();
            // 4. 遍历并绑定点击声音
            foreach (var btn in allButtons)
            {
                btn.RegisterCallback<ClickEvent>(PlayClickAudio);
                btn.RegisterCallback<NavigationSubmitEvent>(PlayClickAudio);
            }
        }
        

        private void OnDisable()
        {
            // 5. 安全注销事件，避免内存泄露风险
            var uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null) return;

            var root = uiDocument.rootVisualElement;
            if (root == null) return;

            var allButtons = root.Query<Button>().ToList();
            foreach (var btn in allButtons)
            {
                btn.UnregisterCallback<ClickEvent>(PlayClickAudio);
                btn.UnregisterCallback<NavigationSubmitEvent>(PlayClickAudio);
            }
        }

        // 播放声音的监听回调
        private void PlayClickAudio(ClickEvent evt)
        {
            if (audioSource != null && clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }

        private void PlayClickAudio(NavigationSubmitEvent evt)
        {
            if (audioSource != null && clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
    }
}
