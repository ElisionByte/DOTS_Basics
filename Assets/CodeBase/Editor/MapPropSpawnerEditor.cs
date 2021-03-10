using CodeBase.Logic.Map;

using UnityEditor;

using UnityEngine;

namespace CodeBase.Editor
{
    [RequireComponent(typeof(MapPropSpawner))]
    public class MapPropSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected | GizmoType.NotInSelectionHierarchy | GizmoType.InSelectionHierarchy)]
        public static void RenderCubeGizmo(MapPropSpawner spawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}