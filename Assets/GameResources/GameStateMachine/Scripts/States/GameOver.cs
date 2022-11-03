using GameResources.Pause;

namespace GameResources.GameStateMachine.Scripts.States
{
    public sealed class GameOver : IState
    {
        public GameOver()
        {
            PauseController.Pause();
        }
    }
}
