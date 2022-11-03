using System;
using GameResources.Cube.Scripts;
using UnityEngine;

namespace GameResources.Coin.Scripts
{
    [RequireComponent(typeof(Collider))]
    public sealed class Coin : MonoBehaviour
    {
        public event Action<int> OnCollected;

        [SerializeField]
        private int value;

        private bool _isCollected;

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected)
            {
                return;
            }
            
            if (other.attachedRigidbody.TryGetComponent(out CubeController _))
            {
                Collect();
            }
        }

        private void Collect()
        {
            _isCollected = true;
            OnCollected?.Invoke(value);

            Destroy(gameObject);
        }
    }
}
