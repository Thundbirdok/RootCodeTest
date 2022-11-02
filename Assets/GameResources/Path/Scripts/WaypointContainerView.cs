using System;
using TMPro;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class WaypointContainerView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private WaypointContainer waypointContainer;
        
        private void OnEnable()
        {
            SetText();

            waypointContainer.OnFreeWaypointChanged += SetText;
        }

        private void OnDisable()
        {
            waypointContainer.OnFreeWaypointChanged -= SetText;
        }

        private void SetText()
        {
            text.text = waypointContainer.FreeWaypointsAmount.ToString();
        }
    }
}
