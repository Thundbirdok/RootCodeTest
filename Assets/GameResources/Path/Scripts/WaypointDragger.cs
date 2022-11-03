using System;
using GameResources.GameStateMachine.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public sealed class WaypointDragger : MonoBehaviour, IDependOnState
    {
        [SerializeField]
        private Pointer pointer;

        public Waypoint SelectedWaypoint { get; private set; }

        public bool IsActiveInThisState => true;
        public Type State => typeof(Game);
        
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
        
        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
        
        /// <summary>
        /// Select waypoint
        /// </summary>
        /// <param name="waypoint"></param>
        public void SelectWaypoint(Waypoint waypoint)
        {
            if (SelectedWaypoint != null)
            {
                return;
            }
            
            SelectedWaypoint = waypoint;

            SelectedWaypoint.transform.position = pointer.PointerPosition;
        }
        
        /// <summary>
        /// Unselect waypoint
        /// </summary>
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
