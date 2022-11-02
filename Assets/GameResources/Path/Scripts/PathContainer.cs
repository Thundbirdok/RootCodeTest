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
            var positions = _path.Select(x => x.transform.position + new Vector3(0, 0.25f, 0)).ToArray();
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        public bool IsPathFinished(int index)
        {
            return index >= _path.Count;
        }
        
        public Vector3 GetPointOnPlane(int index)
        {
            return new Vector3(_path[index].transform.position.x, 0, _path[index].transform.position.z);
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
            
            Destroy(waypoint.gameObject);
        }
    }
}
