using UnityEngine;

namespace Scriptable_objects
{
    [CreateAssetMenu(fileName = "NewAudioConfig", menuName = "UI/Audio Config", order = 0)]
    public class UIElementAudioConfig : ScriptableObject
    {
        public AudioClip buttonClickSound;
    }
}