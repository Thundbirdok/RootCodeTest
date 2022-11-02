using System;
using GameResources.Path.Scripts;
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
        private PathContainer path;

        private bool _isMoving;
        
        private int _pointIndex = 1;

        private void FixedUpdate() => Move();

        public void StartMove()
        {
            SetStartPosition();

            _isMoving = true;
        }

        private void SetStartPosition()
        {
            transform.position = path.GetPointOnPlane(0);
        }
        
        private void Move()
        {
            if (_isMoving == false)
            {
                return;
            }

            var nextPoint = path.GetPointOnPlane(_pointIndex);

            var currentPosition = new Vector2(transform.position.x, transform.position.z);
            var newPosition = Vector2.MoveTowards(currentPosition, nextPoint, speed);

            transform.position = new Vector3(newPosition.x, 0, newPosition.y);

            var sqrMagnitude = (newPosition - nextPoint).sqrMagnitude;

            if (closeEnoughToPointDistance < sqrMagnitude)
            {
                return;
            }

            ++_pointIndex;

            if (path.IsPathFinished(_pointIndex))
            {
                return;
            }

            _isMoving = false;
                
            OnFinish?.Invoke();
        }
    }
}
