using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameResources.Path.Scripts
{
    public class WaypointContainerView : MonoBehaviour, IDragHandler
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private PathController pathController;
        
        private void OnEnable()
        {
            SetText();

            pathController.OnFreeWaypointChanged += SetText;
        }

        private void OnDisable()
        {
            pathController.OnFreeWaypointChanged -= SetText;
        }

        private void SetText()
        {
            text.text = pathController.FreeWaypointsAmount.ToString();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                pathController.CreateWaypoint();

                return;
            }

            pathController.DeleteWaypoint();
        }
    }
}
