using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UIBehavior.SoundBehavior
{
    /// <summary>
    /// Sound behavior implementation for UI Toolkit buttons.
    /// </summary>
    public class ButtonsSoundBehavior : BaseSoundBehavior<Button>
    {
        private readonly EventCallback<ClickEvent> _clickEventHandler;
        private readonly EventCallback<NavigationSubmitEvent> _navigationSubmitEventHandler;

        /// <summary>
        /// Creates button sound callbacks for click and submit input.
        /// </summary>
        /// <param name="audioSource">Playback source.</param>
        /// <param name="audioClip">Clip to play per interaction.</param>
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

        public override void Bind(VisualElement element)
        {
            var buttons = element.Query<Button>().ToList();
            foreach (var button in buttons)
            {
                Bind(button);
            }
        }

        public override void Unbind(VisualElement element)
        {
            var buttons = element.Query<Button>().ToList();
            foreach (var button in buttons)
            {
                Unbind(button);
            }
        }
    }
}