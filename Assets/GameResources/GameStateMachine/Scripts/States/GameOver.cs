using GameResources.Pause;

namespace GameResources.GameStateMachine.Scripts.States
{
    public class GameOver : IState
    {
        public GameOver()
        {
            PauseController.Pause();
        }
    }
}
