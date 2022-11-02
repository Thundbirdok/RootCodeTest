using System;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class WaypointDragger : MonoBehaviour
    {
        [SerializeField]
        private GroundPointer groundPointer;

        public Waypoint SelectedWaypoint { get; private set; }

        private void OnEnable()
        {
            groundPointer.OnPointerUp += UnselectWaypoint;
            groundPointer.OnPointerPositionChanged += DragWaypoint;
        }

        private void OnDisable()
        {
            groundPointer.OnPointerUp -= UnselectWaypoint;
            groundPointer.OnPointerPositionChanged -= DragWaypoint;
        }

        public void SelectWaypoint(Waypoint waypoint)
        {
            SelectedWaypoint = waypoint;

            SelectedWaypoint.transform.position = groundPointer.PointerPosition;
        }
        
        public void UnselectWaypoint()
        {
            SelectedWaypoint = null;
        }

        private void DragWaypoint()
        {
            if (SelectedWaypoint == null)
            {
                return;
            }
            
            SelectedWaypoint.transform.position = groundPointer.PointerPosition;
        }
    }
}
