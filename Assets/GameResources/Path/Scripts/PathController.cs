using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class PathController : MonoBehaviour
    {
        public event Action OnFreeWaypointChanged;

        private int _freeWaypointsAmountAmount;
        public int FreeWaypointsAmount
        {
            get
            {
                return _freeWaypointsAmountAmount;
            }

            private set
            {
                if (_freeWaypointsAmountAmount == value)
                {
                    return;
                }

                _freeWaypointsAmountAmount = value;
                
                OnFreeWaypointChanged?.Invoke();
            }
        }

        public IReadOnlyList<Waypoint> Path => pathContainer.Path;

        public Vector3 StartPosition => pathContainer.StartPosition;
        
        [SerializeField]
        private PathContainer pathContainer;

        [SerializeField]
        private WaypointDragger waypointDragger;

        public void Init(int value)
        {
            FreeWaypointsAmount = value;
            
            pathContainer.Init();
        }

        public bool IsPathFinished(int index) => pathContainer.IsPathFinished(index);
        public Vector3 GetPointOnPlane(int index) => pathContainer.GetPointOnPlane(index);

        public void CreateWaypoint()
        {
            if (FreeWaypointsAmount == 0)
            {
                return;
            }

            if (waypointDragger.SelectedWaypoint != null)
            {
                return;
            }
            
            var draggedWaypoint = pathContainer.AddWaypoint();

            FreeWaypointsAmount--;
            
            waypointDragger.SelectWaypoint(draggedWaypoint);
        }

        public void DeleteWaypoint()
        {
            var waypoint = waypointDragger.SelectedWaypoint;
            
            if (waypoint == null)
            {
                return;
            }

            pathContainer.RemoveWaypoint(waypoint);

            waypointDragger.UnselectWaypoint();

            FreeWaypointsAmount++;
        }
    }
}
