using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SlashController))]
public class SlashControllerEditor : Editor {
    private static Vector2 center, range;
    private static GameObject player;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();


        SerializedObject serializedObject = new SerializedObject(target);
        SlashController controller = (SlashController) target;
        player = controller.player1;

        int level = serializedObject.FindProperty("upgradeLevel").intValue;

        if (level >= 3) {
            range = serializedObject.FindProperty("level3Range").vector2Value;
            center = serializedObject.FindProperty("level3Center").vector2Value;
        } else {
            range = serializedObject.FindProperty("level1Range").vector2Value;
            center = serializedObject.FindProperty("level1Center").vector2Value;
        }
    }

    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    public static void DrawGizmos(SlashController controller, GizmoType type) {
        Gizmos.color = Color.cyan;
        // Gizmos.DrawWireCube(player.transform.position + new Vector3(center.x, center.y, 0), range);
    }
}