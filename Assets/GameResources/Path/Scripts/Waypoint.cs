using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class Waypoint : MonoBehaviour
    {
        private WaypointDragger _waypointDragger;

        public void Construct(WaypointDragger waypointDragger)
        {
            _waypointDragger = waypointDragger;
        }
        
        private void OnMouseDown()
        {
            _waypointDragger.SelectWaypoint(this);
        }
    }
}
