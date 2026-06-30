// ReSharper disable once RedundantUsingDirective
using UnityEngine;

namespace Utility
{
    public static class ExitGame
    {
        public static void Exit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}