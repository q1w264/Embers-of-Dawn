using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIBehavior.SoundBehavior
{
    /// <summary>
    /// Base sound behavior for playing one-shot audio on UI interactions.
    /// </summary>
    /// <typeparam name="T">Element type this sound behavior binds to.</typeparam>
    public abstract class BaseSoundBehavior<T> : IUIBehavior<T> where T : VisualElement
    {
        /// <summary>
        /// Creates a sound behavior.
        /// </summary>
        /// <param name="audioSource">Audio source used for playback.</param>
        /// <param name="audioClip">Clip played on interaction.</param>
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