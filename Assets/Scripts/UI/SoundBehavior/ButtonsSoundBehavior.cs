using UnityEngine;
using UnityEngine.UIElements;

namespace UI.SoundBehavior
{
    public class ButtonsSoundBehavior : SoundBehavior<Button>
    {
        private readonly EventCallback<ClickEvent> _clickEventHandler;
        private readonly EventCallback<NavigationSubmitEvent> _navigationSubmitEventHandler;
        public ButtonsSoundBehavior(AudioSource audioSource, AudioClip audioClip) : base(
            audioSource, audioClip)
        {
            _clickEventHandler = GetSoundBehaviorHandler<ClickEvent>();
            _navigationSubmitEventHandler = GetSoundBehaviorHandler<NavigationSubmitEvent>();
        }

        public override void Bind(Button element)
        {
            element.RegisterCallback(_clickEventHandler);
            element.RegisterCallback(_navigationSubmitEventHandler);
        }

        public override void Unbind(Button element)
        {
            element.UnregisterCallback(_clickEventHandler);
            element.UnregisterCallback(_navigationSubmitEventHandler);
        }
    }
}