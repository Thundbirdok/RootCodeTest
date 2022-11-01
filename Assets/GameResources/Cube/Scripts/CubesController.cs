using System;
using GameResources.UI.Scripts;
using UnityEngine;

namespace GameResources.Cube.Scripts
{
    public class CubesController : MonoBehaviour
    {
        public event Action OnAllCubesFinish;

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
