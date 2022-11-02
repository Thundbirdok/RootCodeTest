using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class PathContainer : MonoBehaviour
    {
        [SerializeField]
        private Waypoint prefab;
        
        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private Transform container;
        
        [SerializeField]
        private LineRenderer lineRenderer;
        
        private readonly List<Waypoint> _path = new List<Waypoint>();

        public void Init()
        {
            _path.Clear();
            
            var waypoint = Instantiate(prefab, startPosition.position, Quaternion.identity, container);
            
            _path.Add(waypoint);
        }

        private void Update()
        {
            lineRenderer.SetPositions(_path.Select(x => x.transform.position).ToArray());
        }

        public bool IsPathFinished(int index)
        {
            return index < _path.Count;
        }
        
        public Vector2 GetPointOnPlane(int index)
        {
            return new Vector2(_path[index].transform.position.x, _path[index].transform.position.z);
        }
        
        public Waypoint AddWaypoint()
        {
            var waypoint = Instantiate(prefab, container);
            
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
        }
    }
}
