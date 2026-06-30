using Core;
using UnityEngine.UIElements;

namespace UI.UIBehavior.ChangeGameStateBehavior
{
    /// <summary>
    /// Button behavior that switches the global game state.
    /// </summary>
    public sealed class ButtonChangeGameStateBehavior : BaseButtonUIBehavior
    {
        private readonly GameState _targetGameState;
        /// <summary>
        /// Creates a behavior that changes to a target game state when triggered.
        /// </summary>
        /// <param name="targetTargetGameState">State to apply.</param>
        public ButtonChangeGameStateBehavior(GameState targetTargetGameState)
        {
            _targetGameState = targetTargetGameState;
            NavigationSubmitEventHandler = GetBehaviorHandler<NavigationSubmitEvent>();
            ClickEventHandler = GetBehaviorHandler<ClickEvent>();
        }
        
        protected override void Behavior()
        {
            GameHub.Get.ChangeGameState(_targetGameState);
        }
    }
}