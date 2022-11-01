using System.Collections.Generic;
using System.Linq;
using GameResources.Cube.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using UnityEngine;

namespace GameResources.GameStateMachine.Scripts
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField]
        private CubesController cubesController;

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
            
            State = new Game();
            
            cubesController.OnAllCubesFinish += SetGameOverState;
        }

        private void OnDisable()
        {
            cubesController.OnAllCubesFinish -= SetGameOverState;
        }

        private void SetGameOverState()
        {
            State = new GameOver();
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
