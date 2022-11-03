using GameResources.Pause;

namespace GameResources.GameStateMachine.Scripts.States
{
    public sealed class Game : IState
    {
        public Game()
        {
            PauseController.Unpause();
        }
    }
}
