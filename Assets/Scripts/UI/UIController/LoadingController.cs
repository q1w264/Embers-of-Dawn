using UnityEngine.UIElements;

namespace UI.UIController
{
    public class LoadingController : BaseUIController
    {
        private ProgressBar _progressBar;
        private void Start()
        {
            _progressBar = Root.Q<ProgressBar>("loading__progress-bar");
            _progressBar.value = 0;
        }

        private void FixedUpdate()
        {
            _progressBar.value += 1f;
        }
    }
}