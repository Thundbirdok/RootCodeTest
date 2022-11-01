using System;
using GameResources.Cube.Scripts;
using UnityEngine;

namespace GameResources.Coin.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour
    {
        public event Action OnCollected;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CubeController cube))
            {
                Collect();
            }
        }

        private void Collect()
        {
            OnCollected?.Invoke();

            Destroy(gameObject);
        }
    }
}
