using GameResources.Pause;

namespace GameResources.GameStateMachine.Scripts.States
{
    public sealed class Pause : IState
    {
        public Pause()
        {
            PauseController.Pause();
        }
    }
}
