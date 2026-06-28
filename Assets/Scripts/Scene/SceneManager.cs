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

        private IEnumerator LoadSceneAsyncRoutine(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (operation is { isDone: false })
            {
                _uiManager.Open("Loading");
                yield return null;
            }
        }
    }
}