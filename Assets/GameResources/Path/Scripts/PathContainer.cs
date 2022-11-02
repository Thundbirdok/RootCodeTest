using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameResources.Path.Scripts
{
    [Serializable]
    public class PathContainer
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

        public void Init()
        {
            Clear();

            CreateStartPoint();
        }

        public bool IsPathFinished(int index)
        {
            return index >= _path.Count;
        }
        
        public Vector3 GetPointOnPlane(int index)
        {
            return new Vector3
            (
                _path[index].transform.position.x,
                0, 
                _path[index].transform.position.z
            );
        }
        
        public Waypoint AddWaypoint()
        {
            var waypoint = Object.Instantiate(prefab, container);
            
            _path.Add(waypoint);

            return waypoint;
        }

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
