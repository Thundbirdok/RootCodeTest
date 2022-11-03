using System;
using GameResources.GameStateMachine.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameResources.Path.Scripts
{
    using Ground.Scripts;
    
    public sealed class Pointer : MonoBehaviour, IDependOnState
    {
        public event Action OnPointerPositionChanged;
        public event Action OnPointerUp;

        public event Action<Waypoint> OnPointerDownOnWaypoint;

        public bool IsActiveInThisState => true;
        public Type State => typeof(Game);
        
        private Vector3 _pointerPosition;
        public Vector3 PointerPosition
        {
            get
            {
                return _pointerPosition;
            }

            private set
            {
                if (_pointerPosition == value)
                {
                    return;
                }

                _pointerPosition = value;
                
                OnPointerPositionChanged?.Invoke();
            }
        }
        
        [SerializeField]
        private Camera raycastCamera;
        
        private const float MAX_RAYCAST_DISTANCE = 50;
        
        private readonly RaycastHit[] _hits = new RaycastHit[5];

        private void Update() => CheckPointerPosition();

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);

        private void CheckPointerPosition()
        {
            if (Input.GetMouseButton(0) == false)
            {
                OnPointerUp?.Invoke();
                
                return;
            }
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Raycast();
        }

        private void Raycast()
        {
            var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);

            var size = Physics.RaycastNonAlloc(ray, _hits, MAX_RAYCAST_DISTANCE);
            
            if (size == 0)
            {
                return;
            }

            GetHits(size);
        }
        
        private void GetHits(in int size)
        {
            for (var i = 0; i < size; ++i)
            {
                if (_hits[i].collider.TryGetComponent(out Ground _))
                {
                    PointerPosition = _hits[i].point;
                }

                if (_hits[i].collider.TryGetComponent(out Waypoint waypoint))
                {
                    OnPointerDownOnWaypoint?.Invoke(waypoint);
                }
            }
        }
    }
}
