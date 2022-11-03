using System;
using GameResources.Path.Scripts;
using UnityEngine;

namespace GameResources.Cube.Scripts
{
    public sealed class CubeController : MonoBehaviour
    {
        public event Action OnFinish;
        
        [SerializeField]
        private float closeEnoughToPointDistance = 0.01f;

        [SerializeField] [Range(0, 1)]
        private float speed;
    
        [SerializeField]
        private PathController pathController;

        private bool _isMoving;
        
        private int _pointIndex = 1;

        private void FixedUpdate() => Move();

        /// <summary>
        /// Set cube on start position of path
        /// </summary>
        public void SetStartPosition()
        {
            transform.position = pathController.StartPosition;
            _pointIndex = 1;
        }
        
        /// <summary>
        /// Start moving on the path
        /// </summary>
        public void StartMove() => _isMoving = true;

        private void Move()
        {
            if (_isMoving == false)
            {
                return;
            }

            if (pathController.IsPathFinished(_pointIndex))
            {
                _isMoving = false;
                
                OnFinish?.Invoke();
                
                return;
            }
            
            var nextPoint = pathController.GetPointOnPlane(_pointIndex);
            
            var newPosition = Vector3.MoveTowards
            (
                transform.position, 
                nextPoint, 
                speed
            );

            transform.position = newPosition;

            var sqrMagnitude = (newPosition - nextPoint).sqrMagnitude;

            if (closeEnoughToPointDistance > sqrMagnitude)
            {
                ++_pointIndex;
            }
        }
    }
}
