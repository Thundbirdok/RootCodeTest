using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameResources.Path.Scripts
{
    [Serializable]
    public sealed class PathContainer
    {
        private readonly List<Waypoint> _path = new List<Waypoint>();
        public IReadOnlyList<Waypoint> Path => _path;
        
        [SerializeField]
        private Waypoint prefab;
        
        [SerializeField]
        private Transform startPoint;

        public Vector3 StartPosition => startPoint.position;
        
        [SerializeField]
        private Transform container;

        /// <summary>
        /// Initialize path
        /// </summary>
        public void Init()
        {
            Clear();

            CreateStartPoint();
        }

        /// <summary>
        /// Is waypoint with this index exist
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>true if waypoint exist</returns>
        public bool IsPathFinished(int index) => index >= _path.Count;

        /// <summary>
        /// Get position of waypoint with this index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Position of waypoint</returns>
        public Vector3 GetWaypointPositionOnGround(int index)
        {
            return new Vector3
            (
                _path[index].transform.position.x,
                0, 
                _path[index].transform.position.z
            );
        }
        
        /// <summary>
        /// Create and add waypoint to path
        /// </summary>
        /// <returns>Created waypoint</returns>
        public Waypoint AddWaypoint()
        {
            var waypoint = Object.Instantiate(prefab, container);
            
            _path.Add(waypoint);

            return waypoint;
        }

        /// <summary>
        /// Remove this waypoint from path
        /// </summary>
        /// <param name="waypoint">Waypoint</param>
        public void RemoveWaypoint(Waypoint waypoint)
        {
            if (_path.Contains(waypoint) == false)
            {
                return;
            }
            
            _path.Remove(waypoint);
            
            Object.Destroy(waypoint.gameObject);
        }

        private void Clear()
        {
            foreach (var waypoint in _path)
            {
                Object.Destroy(waypoint.gameObject);
            }
            
            _path.Clear();
        }

        private void CreateStartPoint()
        {
            var waypoint = Object.Instantiate
            (
                prefab, 
                startPoint.position, 
                Quaternion.identity, 
                container
            );
            
            _path.Add(waypoint);
        }
    }
}
