using System.Collections;
using System.Collections.Generic;
using Core;
using UI.UIController;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Scene
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private GameState targetGameState;
        [SerializeField] private List<BaseUIController> uiControllers;
        [SerializeField] private string firstOpenedUI;
        private UIManager _uiManager;

        protected override void Awake()
        {
            base.Awake();
            _uiManager = new UIManager(uiControllers);
            GameHub.Get.ChangeGameState(targetGameState);
        }

        private void Start()
        {
            _uiManager.Open(firstOpenedUI);
        }

        public void GoToScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsyncRoutine(sceneName));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator LoadSceneAsyncRoutine(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            
            if(operation == null)
                Debug.LogError($"Could not load scene '{sceneName}'. No scene loaded.");
            
            if (operation == null) yield break;
            // operation.allowSceneActivation = false;
            
            _uiManager.Open("Loading");
            
            while (operation is { isDone: false })
            {
                yield return null;
            }
        }
    }
}