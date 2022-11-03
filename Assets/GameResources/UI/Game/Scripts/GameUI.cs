using System;
using GameResources.Path.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.UI.Game.Scripts
{
    public sealed class GameUI : MonoBehaviour
    {
        public event Action OnStartClicked;
        public event Action OnPauseClicked;

        [SerializeField]
        private WaypointContainerView waypointContainerViewA;

        [SerializeField]
        private WaypointContainerView waypointContainerViewB;
        
        [SerializeField]
        private Button start;

        [SerializeField]
        private Button pause;

        /// <summary>
        /// Construct ui
        /// </summary>
        /// <param name="a">path A</param>
        /// <param name="b">path B</param>
        public void Construct(PathController a, PathController b)
        {
            waypointContainerViewA.SetPathController(a);
            waypointContainerViewB.SetPathController(b);
        }
        
        private void OnEnable()
        {
            start.onClick.AddListener(InvokeOnStartClick);
            pause.onClick.AddListener(InvokePauseClick);
            
            start.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            start.onClick.RemoveListener(InvokeOnStartClick);
            pause.onClick.RemoveListener(InvokePauseClick);
        }

        private void InvokeOnStartClick()
        {
            start.gameObject.SetActive(false);
            
            OnStartClicked?.Invoke();
        }

        private void InvokePauseClick() => OnPauseClicked?.Invoke();
    }
}
