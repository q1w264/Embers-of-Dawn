using System;
using UnityEngine;
using Utility;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        private GameState CurrentState { get; set; } = GameState.Boot;

        public static event Action<GameState> OnGameStateChanged;

        private void Start()
        {
            CurrentState = GameState.MainMenu;
        }

        public void ChangeState(GameState newState)
        {
            CurrentState = newState;
            switch (newState)
            {
                case GameState.Playing:
                    Time.timeScale = 1;
                    break;
                case GameState.Quiting:
                case GameState.MainMenu:
                case GameState.Paused:
                case GameState.GameOver:
                    Time.timeScale = 0;
                    break;
                case GameState.Boot:
                case GameState.Loading:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnGameStateChanged?.Invoke(CurrentState);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
        
    }
    
    public enum GameState
    {
        Boot,
        MainMenu,
        Loading,
        Playing,
        Paused,
        GameOver,
        Quiting
    }
}