using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIBehavior.SoundBehavior
{
    public abstract class BaseSoundBehavior<T> : IUIBehavior<T>
    {
        protected BaseSoundBehavior(AudioSource audioSource, AudioClip audioClip)
        {
            _audioSource = audioSource;
            _audioClip = audioClip;
        }
        
        private readonly AudioSource _audioSource;
        private readonly AudioClip _audioClip;
        
        public abstract void Bind(T element);
        
        public abstract void Unbind(T element);
        
        public abstract void Bind(VisualElement element);
        
        public abstract void Unbind(VisualElement element);
        
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