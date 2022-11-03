using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameResources.Spawner.Scripts
{
    [Serializable]
    public sealed class Spawner
    {
        private readonly List<GameObject> _spawnedObjects = new List<GameObject>();
        public IReadOnlyList<GameObject> SpawnedObjects => _spawnedObjects;
        
        [SerializeField] [Range(1, 1000)]
        private int amount;

        [SerializeField]
        private SpawnZoneBox spawnZone;
    
        [SerializeField]
        private Transform container;
    
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private int maxSpawnPerFrame = 10;

        private Vector2 _min;
        private Vector2 _max;
        
        /// <summary>
        /// Spawn objects in zone
        /// </summary>
        public async Task Spawn()
        {
            Clear();
            
            var spawnPerFrameCount = 0;
            
            _min = new Vector2(spawnZone.LeftDown.x, spawnZone.LeftDown.z);
            _max = new Vector2(spawnZone.RightTop.x, spawnZone.RightTop.z);
            
            for (var i = 0; i < amount; ++i)
            {
                SpawnObject();

                spawnPerFrameCount++;
                
                if (spawnPerFrameCount != maxSpawnPerFrame)
                {
                    continue;
                }

                spawnPerFrameCount = 0;

                await Task.Yield();
            }
        }

        private void SpawnObject()
        {
            var x = Random.Range(_min.x, _max.x);
            var z = Random.Range(_min.y, _max.y);

            var position = new Vector3(x, 0, z);

            position = spawnZone.transform.TransformPoint(position);

            var spawnedObject = Object.Instantiate(prefab, position, Quaternion.identity, container);

            _spawnedObjects.Add(spawnedObject);
        }

        private void Clear()
        {
            foreach (var spawned in _spawnedObjects)
            {
                Object.Destroy(spawned);
            }
            
            _spawnedObjects.Clear();
        }
    }
}
