using UnityEngine.UIElements;

namespace UI.UIController
{
    public class TitleController : SoundedUIController
    {
        private void Start()
        {
            var startButton = Root.Q<Button>("start-button");
            FocusFirst(startButton);
            Open();
        }
    }
}