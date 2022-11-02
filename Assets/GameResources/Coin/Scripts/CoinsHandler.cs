using System.Collections.Generic;
using GameResources.Score.Scripts;
using UnityEngine;

namespace GameResources.Coin.Scripts
{
    using GameResources.Spawner.Scripts;
    
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField]
        private Spawner coinsSpawner;

        [SerializeField]
        private ScoreHandler scoreHanler;
        
        private readonly List<Coin> _coins = new List<Coin>();

        public async void Spawn()
        {
            await coinsSpawner.Spawn();
            
            GetCoins();
        }
        
        private void GetCoins()
        {
            CleanCoins();
            
            foreach (var spawnedObject in coinsSpawner.SpawnedObjects)
            {
                if (spawnedObject.TryGetComponent(out Coin coin) == false)
                {
                    continue;
                }

                coin.OnCollected += AddPoints;
                    
                _coins.Add(coin);
            }
        }

        private void CleanCoins()
        {
            foreach (var coin in _coins)
            {
                coin.OnCollected -= AddPoints;
            }
            
            _coins.Clear();
        }

        private void AddPoints(int value)
        {
            scoreHanler.AddPoints(value);
        }
    }
}
