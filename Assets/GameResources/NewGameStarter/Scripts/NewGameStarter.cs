using GameResources.Coin.Scripts;
using GameResources.Cube.Scripts;
using GameResources.Path.Scripts;
using UnityEngine;

namespace GameResources.NewGameStarter.Scripts
{
    public class NewGameStarter : MonoBehaviour
    {
        [SerializeField]
        private CubesController cubesController;

        [SerializeField]
        private CoinsHandler coinsHandler;
        
        [SerializeField]
        private PathController[] pathControllers;

        [Header("Waypoints amount range")]
        [SerializeField]
        private int min = 3;
        
        [SerializeField]
        private int max = 7;
        
        public void StartNewGame()
        {
            cubesController.ResetCubes();

            coinsHandler.Spawn();
            
            foreach (var pathController in pathControllers)
            {
                pathController.Init(Random.Range(min, max));
            }
        }
    }
}
