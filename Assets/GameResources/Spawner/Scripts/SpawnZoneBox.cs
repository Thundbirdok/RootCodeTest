using UnityEngine;

namespace GameResources.Spawner.Scripts
{
    public class SpawnZoneBox : MonoBehaviour
    {
        [SerializeField]
        private Transform leftDown;

        public Vector3 LeftDown => leftDown.localPosition;
        
        [SerializeField]
        private Transform rightTop;
        public Vector3 RightTop => rightTop.localPosition;
        
        public Vector2 Size
        {
            get
            {
                if (leftDown == null || rightTop == null)
                {
                    return Vector2.zero;
                }

                var rightTopPosition = rightTop.localPosition;
                var rightTopV2 = new Vector2(rightTopPosition.x, rightTopPosition.z);
                
                var leftDownPosition = leftDown.localPosition;
                var leftDownV2 = new Vector2(leftDownPosition.x, leftDownPosition.z);
                
                return rightTopV2 - leftDownV2;
            }
        }
    }
}