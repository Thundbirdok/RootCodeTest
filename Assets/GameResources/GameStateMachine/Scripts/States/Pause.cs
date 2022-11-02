using GameResources.Pause;

namespace GameResources.GameStateMachine.Scripts.States
{
    public class Pause : IState
    {
        public Pause()
        {
            PauseController.Pause();
        }
    }
}
