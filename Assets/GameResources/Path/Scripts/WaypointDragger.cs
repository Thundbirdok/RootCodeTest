using System;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class WaypointDragger : MonoBehaviour
    {
        [SerializeField]
        private Pointer pointer;

        public Waypoint SelectedWaypoint { get; private set; }

        private void OnEnable()
        {
            pointer.OnPointerUp += UnselectWaypoint;
            pointer.OnPointerPositionChanged += DragWaypoint;
            pointer.OnPointerDownOnWaypoint += SelectWaypoint;
        }

        private void OnDisable()
        {
            pointer.OnPointerUp -= UnselectWaypoint;
            pointer.OnPointerPositionChanged -= DragWaypoint;
            pointer.OnPointerDownOnWaypoint -= SelectWaypoint;
        }

        public void SelectWaypoint(Waypoint waypoint)
        {
            SelectedWaypoint = waypoint;

            SelectedWaypoint.transform.position = pointer.PointerPosition;
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
            
            SelectedWaypoint.transform.position = pointer.PointerPosition;
        }
    }
}
