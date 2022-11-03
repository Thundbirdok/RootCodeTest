using System.Collections.Generic;
using System.Linq;
using GameResources.Cube.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using GameResources.UI.Game.Scripts;
using GameResources.UI.GameOver.Scripts;
using GameResources.UI.Pause.Scripts;
using UnityEngine;

namespace GameResources.GameStateMachine.Scripts
{
    using NewGameStarter.Scripts;
    
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField]
        private CubesController cubesController;

        [SerializeField]
        private GameUIProvider gameUI;
        
        [SerializeField]
        private PauseUIProvider pauseUI;

        [SerializeField]
        private GameOverUIProvider gameOverUi;
        
        [SerializeField]
        private NewGameStarter newGameStarter;
        
        private IState _state;
        private IState State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;

                CheckState();
            }
        }
        
        private List<IDependOnState> _depends = new List<IDependOnState>(); 

        private void OnEnable()
        {
            GetAllDependedOnState();

            StartNewGame();
            
            gameUI.OnPauseClicked += SetPauseState;
            pauseUI.OnContinueClicked += SetGameState;
            gameOverUi.OnPlayAgainClicked += StartNewGame;
            cubesController.OnAllCubesFinish += SetGameOverState;
        }

        private void OnDisable()
        {
            gameUI.OnPauseClicked -= SetPauseState;
            pauseUI.OnContinueClicked -= SetGameState;
            gameOverUi.OnPlayAgainClicked -= StartNewGame;
            cubesController.OnAllCubesFinish -= SetGameOverState;
        }

        private void SetGameState() => State = new Game();

        private void SetPauseState() => State = new States.Pause();

        private void SetGameOverState() => State = new GameOver();

        private void StartNewGame()
        {
            newGameStarter.StartNewGame();
            
            State = new Game();
        }
        
        private void GetAllDependedOnState()
        {
            _depends = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<IDependOnState>()
                .ToList();
        }
        
        private void CheckState()
        {
            foreach (var depended in _depends)
            {
                if ((depended.State == _state.GetType()) == depended.IsActiveInThisState)
                {
                    depended.Activate();
                    
                    continue;
                }
                
                depended.Deactivate();
            }
        }
    }
}
