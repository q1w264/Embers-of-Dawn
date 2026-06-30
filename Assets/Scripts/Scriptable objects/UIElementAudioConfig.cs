using UnityEngine;

namespace Scriptable_objects
{
    /// <summary>
    /// Audio config asset for UI interaction sound effects.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioConfig", menuName = "UI/Audio Config", order = 0)]
    public class UIElementAudioConfig : ScriptableObject
    {
        /// <summary>
        /// One-shot clip played when UI button interactions occur.
        /// </summary>
        public AudioClip buttonClickSound;
    }
}