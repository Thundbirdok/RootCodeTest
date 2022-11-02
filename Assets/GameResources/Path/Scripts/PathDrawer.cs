using System.Linq;
using UnityEngine;

namespace GameResources.Path.Scripts
{
    public class PathDrawer : MonoBehaviour
    {
        [SerializeField]
        private PathController pathController;

        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private float lineHeight = 0.25f;
        
        private void Update()
        {
            var positions = pathController.Path
                .Select
                (
                    x => x.transform.position + new Vector3(0, lineHeight, 0)
                )
                .ToArray();

            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
    }
}
