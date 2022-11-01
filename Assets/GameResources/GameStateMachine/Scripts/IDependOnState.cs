using System;

namespace GameResources.GameStateMachine.Scripts
{
    public interface IDependOnState
    {
        public bool IsActiveInThisState { get; }

        public Type State { get; }

        public void Activate();
        
        public void Deactivate();
    }
}
