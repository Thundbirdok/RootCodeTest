using System;

namespace GameResources.GameStateMachine.Scripts
{
    public interface IDependOnState
    {
        /// <summary>
        /// Is Active in this state
        /// </summary>
        public bool IsActiveInThisState { get; }

        /// <summary>
        /// State on which depends
        /// </summary>
        public Type State { get; }

        /// <summary>
        /// Initialize and activate object
        /// </summary>
        public void Activate();
        
        /// <summary>
        /// Deactivate object
        /// </summary>
        public void Deactivate();
    }
}
