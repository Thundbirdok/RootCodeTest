using UnityEditor;
using UnityEngine;

namespace GameResources.Spawner.Scripts.Editor
{
    public sealed class SpawnZoneBoxInspector : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.InSelectionHierarchy)]
        private static void DrawGridPlane(SpawnZoneBox box, GizmoType gizmoType)
        {
            var gizmoDefaultColor = Gizmos.color;
            Gizmos.color = Color.cyan;
            
            var gizmoDefaultMatrix = Gizmos.matrix;
            Gizmos.matrix = box.transform.localToWorldMatrix;
            
            var size = new Vector3(box.Size.x, 0, box.Size.y);
            
            Gizmos.DrawWireCube
            (
                Vector3.zero,
                size
            );
            
            Gizmos.matrix = gizmoDefaultMatrix;            
            
            Gizmos.color = gizmoDefaultColor;
        }
    }
}
