using UnityEngine.UIElements;
using Utility;

namespace UI.UIBehavior.ExitGameBehavior
{
    /// <summary>
    /// Button behavior that exits play mode or application.
    /// </summary>
    public sealed class ButtonExitGameBehavior : BaseButtonUIBehavior
    {
        public ButtonExitGameBehavior()
        {
            NavigationSubmitEventHandler = GetBehaviorHandler<NavigationSubmitEvent>();
            ClickEventHandler = GetBehaviorHandler<ClickEvent>();
        }

        protected override void Behavior()
        {
            ExitGame.Exit();
        }
    }
}