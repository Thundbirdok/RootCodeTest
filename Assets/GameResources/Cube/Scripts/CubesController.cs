using System;
using GameResources.GameStateMachine.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using GameResources.UI.Scripts;
using UnityEngine;

namespace GameResources.Cube.Scripts
{
    public class CubesController : MonoBehaviour, IDependOnState
    {
        public event Action OnAllCubesFinish;

        public bool IsActiveInThisState => true;
        public Type State => typeof(Game);
        
        [SerializeField]
        private CubeController[] cubes;

        [SerializeField]
        private GameUI gameUi;
        
        private int _finishedCubes = 0;
        
        private void OnEnable()
        {
            gameUi.OnStartClicked += StartCubes;
            
            foreach (var cube in cubes)
            {
                cube.OnFinish += CubeFinished;
            }
        }

        private void OnDisable()
        {
            gameUi.OnStartClicked -= StartCubes;
            
            foreach (var cube in cubes)
            {
                cube.OnFinish -= CubeFinished;
            }
        }

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);

        public void ResetCubes()
        {
            _finishedCubes = 0;
            
            foreach (var cube in cubes)
            {
                cube.SetStartPosition();
            }
        }
        
        private void CubeFinished()
        {
            _finishedCubes++;

            if (_finishedCubes == cubes.Length)
            {
                OnAllCubesFinish?.Invoke();
            }
        }

        private void StartCubes()
        {
            foreach (var cube in cubes)
            {
                cube.StartMove();
            }
        }
    }
}
