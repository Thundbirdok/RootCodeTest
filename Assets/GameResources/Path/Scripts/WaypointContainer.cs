using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace GameResources.Path.Scripts
{
    public class WaypointContainer : MonoBehaviour, IDragHandler
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

        [SerializeField]
        private PathContainer pathContainer;

        [SerializeField]
        private WaypointDragger waypointDragger;
        
        private void OnEnable()
        {
            pathContainer.Init();

            FreeWaypointsAmount = Random.Range(3, 7);
        }

        private void CreateWaypoint()
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
            
            draggedWaypoint.Construct(waypointDragger);
            
            FreeWaypointsAmount--;
            
            waypointDragger.SelectWaypoint(draggedWaypoint);
        }

        private void DeleteWaypoint()
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

        public void OnDrag(PointerEventData eventData)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                CreateWaypoint();

                return;
            }

            DeleteWaypoint();
        }
    }
}
