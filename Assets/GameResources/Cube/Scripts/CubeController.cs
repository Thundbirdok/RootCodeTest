using System;
using UnityEngine;

namespace GameResources.Cube.Scripts
{
    public class CubeController : MonoBehaviour
    {
        public event Action OnFinish;
        
        [SerializeField]
        private float closeEnoughToPointDistance = 0.01f;

        [SerializeField] [Range(0, 1)]
        private float speed;
    
        [SerializeField]
        private Transform[] path;

        private bool _isMoving = false;
        
        private int _pointIndex = 1;

        private void FixedUpdate() => Move();

        public void StartMove()
        {
            SetStartPosition();

            _isMoving = true;
        }

        private void SetStartPosition()
        {
            transform.position = GetPointOnPlane(0);
        }
        
        private void Move()
        {
            if (_isMoving == false)
            {
                return;
            }

            var nextPoint = GetPointOnPlane(_pointIndex);

            var currentPosition = new Vector2(transform.position.x, transform.position.z);
            var newPosition = Vector2.MoveTowards(currentPosition, nextPoint, speed);

            transform.position = new Vector3(newPosition.x, 0, newPosition.y);

            var sqrMagnitude = (newPosition - nextPoint).sqrMagnitude;

            if (closeEnoughToPointDistance < sqrMagnitude)
            {
                return;
            }

            ++_pointIndex;

            if (_pointIndex < path.Length)
            {
                return;
            }

            _isMoving = false;
                
            OnFinish?.Invoke();
        }

        private Vector2 GetPointOnPlane(int index)
        {
            return new Vector2(path[index].position.x, path[index].position.z);
        }
    }
}
