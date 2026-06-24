using System;
using UnityEngine;

namespace UI.SoundBehavior
{
    public abstract class SoundBehavior
    {
        protected static Action<T> GetSoundBehaviorHandler<T>(AudioSource audioSource,AudioClip audioClip)
        {
            return _ =>
            {
                if (audioSource != null && audioClip != null)
                {
                    audioSource.PlayOneShot(audioClip);
                }
            };
        }
    }
}