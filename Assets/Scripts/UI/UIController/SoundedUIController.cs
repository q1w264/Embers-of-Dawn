using UI.UIBehavior.SoundBehavior;
using UnityEngine;

namespace UI.UIController
{
    /// <summary>
    /// Base UI controller that adds click/submit sound behavior for buttons.
    /// </summary>
    public class SoundedUIController : BaseUIController
    {
        [field: Header("Sound Behavior")]
        [field: SerializeField]private AudioSource AudioSource { get; set; }
        [SerializeField]private Scriptable_objects.UIElementAudioConfig audioConfig;
        
        private ButtonsSoundBehavior _buttonsSoundBehavior;

        protected override void Awake()
        {
            base.Awake();
            _buttonsSoundBehavior = new ButtonsSoundBehavior(AudioSource, audioConfig.buttonClickSound);
        }

        protected virtual void OnEnable()
        {
            _buttonsSoundBehavior?.Bind(Root);
        }

        protected virtual void OnDisable()
        {
            _buttonsSoundBehavior?.Unbind(Root);
        }

        protected virtual void OnDestroy()
        {
            _buttonsSoundBehavior?.Unbind(Root);
        }
    }
}