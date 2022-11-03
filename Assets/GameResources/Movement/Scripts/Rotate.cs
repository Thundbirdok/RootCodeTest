using UnityEngine;
using Random = UnityEngine.Random;

namespace GameResources.Movement.Scripts
{
    public sealed class Rotate : MonoBehaviour
    {
        [SerializeField]
        private Vector3 speed;

        [SerializeField]
        private bool isStartAtRandomRotation;

        private void Start()
        {
            if (isStartAtRandomRotation)
            {
                transform.Rotate(speed.normalized * Random.Range(0f, 360f));
            }
        }

        private void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}
