using UnityEngine;
using Random = UnityEngine.Random;

namespace GameResources.Spawner.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] [Range(1, 1000)]
        private int amount;

        [SerializeField]
        private SpawnZoneBox spawnZone;
    
        [SerializeField]
        private Transform container;
    
        [SerializeField]
        private GameObject prefab;
    
        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var min = new Vector2(spawnZone.LeftDown.x, spawnZone.LeftDown.z);
            var max = new Vector2(spawnZone.RightTop.x, spawnZone.RightTop.z);
            
            for (var i = 0; i < amount; ++i)
            {
                var x = Random.Range(min.x, max.x);
                var z = Random.Range(min.y, max.y);
            
                var position = new Vector3(x, 0, z);

                position = spawnZone.transform.TransformPoint(position);
                
                Instantiate(prefab, position, Quaternion.identity, container);
            }
        }
    }
}
