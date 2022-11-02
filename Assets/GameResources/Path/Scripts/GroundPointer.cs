using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameResources.Path.Scripts
{
    using Ground.Scripts;
    
    public class GroundPointer : MonoBehaviour
    {
        public event Action OnPointerPositionChanged;
        public event Action OnPointerUp;

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

        private void CheckPointerPosition()
        {
            if (Input.GetMouseButton(0) == false)
            {
                OnPointerUp?.Invoke();
                
                return;
            }

            var isOverUI = EventSystem.current.IsPointerOverGameObject();

            if (isOverUI)
            {
                return;
            }
            
            if (TryGetPoint(out var point))
            {
                PointerPosition = point;
            }
        }

        private bool TryGetPoint(out Vector3 point)
        {
            var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);

            var size = Physics.RaycastNonAlloc(ray, _hits, MAX_RAYCAST_DISTANCE);
            
            if (size == 0)
            {
                point = Vector3.zero;
                
                return false;
            }
            
            if (TryGetHitOnGround(size, out var hit) == false)
            {
                point = Vector3.zero;
                
                return false;
            }

            point = hit.point;

            return true;
        }
        
        private bool TryGetHitOnGround(in int size, out RaycastHit hit)
        {
            hit = new RaycastHit();

            for (var i = 0; i < size; ++i)
            {
                if (_hits[i].collider.TryGetComponent(out Ground _) == false)
                {
                    continue;
                }

                hit = _hits[i];

                return true;
            }

            return false;
        }
    }
}
