using System;

namespace Core
{
    public abstract class BaseManager
    {
        private readonly GameHub _gameHub;
        
        protected BaseManager(GameHub gameHub)
        {
            _gameHub = gameHub;
        }

        protected void RegisterGameState(GameState gameState, Action callback)
        {
            _gameHub.OnGameStateChanged += state =>
            {
                if (state == gameState)
                {
                    callback?.Invoke();
                }
            };
        }
    }
}