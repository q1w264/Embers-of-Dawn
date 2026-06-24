using UnityEngine;
using UnityEngine.UIElements;

namespace UI.SoundBehavior
{
    public abstract class SoundBehavior<T> : IUIBehavior<T>
    {
        protected SoundBehavior(AudioSource audioSource, AudioClip audioClip)
        {
            _audioSource = audioSource;
            _audioClip = audioClip;
        }
        
        private readonly AudioSource _audioSource;
        private readonly AudioClip _audioClip;
        
        public abstract void Bind(T element);
        
        public abstract void Unbind(T element);
        
        protected EventCallback<TCtx> GetSoundBehaviorHandler<TCtx>()
        {
            return _ =>
            {
                if (_audioSource != null && _audioClip != null)
                {
                    _audioSource.PlayOneShot(_audioClip);
                }
            };
        }
    }
}