using GameResources.Coin.Scripts;
using GameResources.Cube.Scripts;
using GameResources.Path.Scripts;
using GameResources.Score.Scripts;
using UnityEngine;

namespace GameResources.NewGameStarter.Scripts
{
    public sealed class NewGameStarter : MonoBehaviour
    {
        [SerializeField]
        private CubesController cubesController;

        [SerializeField]
        private CoinsHandler coinsHandler;

        [SerializeField]
        private ScoreHandler scoreHandler;
        
        [SerializeField]
        private PathController[] pathControllers;

        [Header("Waypoints amount range")]
        [SerializeField]
        private int min = 3;
        
        [SerializeField]
        private int max = 7;
        
        /// <summary>
        /// Initialize new game
        /// </summary>
        public void StartNewGame()
        {
            cubesController.ResetCubes();

            coinsHandler.Spawn();
            
            scoreHandler.ResetCurrentScore();
            
            foreach (var pathController in pathControllers)
            {
                pathController.Init(Random.Range(min, max));
            }
        }
    }
}
