using UI.UIBehavior.SoundBehavior;
using UnityEngine;

namespace UI.UIController
{
    public class SoundedUIController : BaseUIController
    {   
        [Header("Sound Behavior")] 
        [SerializeField]private AudioSource audioSource;
        [SerializeField]private Scriptable_objects.UIElementAudioConfig audioConfig;
        
        private ButtonsSoundBehavior _buttonsSoundBehavior;

        protected override void Awake()
        {
            base.Awake();
            _buttonsSoundBehavior = new ButtonsSoundBehavior(audioSource, audioConfig.buttonClickSound);
        }

        protected virtual void OnEnable()
        {
            _buttonsSoundBehavior?.Bind(Root);
        }

        protected virtual void OnDisable()
        {
            _buttonsSoundBehavior?.Unbind(Root);
        }

    }
}