using System.Collections;
using System.Collections.Generic;
using Core;
using UI.UIController;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    /// <summary>
    /// Base scene flow manager responsible for initial game state and scene transitions.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameState targetGameState;
        [SerializeField] private List<BaseUIController> uiControllers;
        [SerializeField] private string firstOpenedUI;
        protected UIManager UIManager;
        private bool _isSceneLoading;

        protected virtual void Awake()
        {
            UIManager = new UIManager(uiControllers);
            GameHub.Get.ChangeGameState(targetGameState);
        }
        
        protected virtual void Start()
        {
            if (!string.IsNullOrEmpty(firstOpenedUI))
                UIManager.Open(firstOpenedUI);
        }

        /// <summary>
        /// Loads a new scene asynchronously and shows loading UI.
        /// </summary>
        /// <param name="sceneName">Target scene name.</param>
        public void GoToScene(string sceneName)
        {
            if (_isSceneLoading) return;
            _isSceneLoading = true;
            StartCoroutine(LoadSceneAsyncRoutine(sceneName));
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator LoadSceneAsyncRoutine(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            
            if(operation == null)
                Debug.LogError($"Could not load scene '{sceneName}'. No scene loaded.");
            
            if (operation == null)
            {
                _isSceneLoading = false;
                yield break;
            }
            // operation.allowSceneActivation = false;
            
            UIManager.Open("Loading");
            
            while (operation is { isDone: false })
            {
                yield return null;
            }
        }
    }
}