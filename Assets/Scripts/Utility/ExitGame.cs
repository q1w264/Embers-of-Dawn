// ReSharper disable once RedundantUsingDirective
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Provides a unified application-exit entry for editor and player builds.
    /// </summary>
    public static class ExitGame
    {
        /// <summary>
        /// Exits play mode in editor or quits the standalone player.
        /// </summary>
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