using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (MovingBlock)), CanEditMultipleObjects]
public class MovingBlockTarget : Editor
{
    protected virtual void OnSceneGUI()
    {
        MovingBlock block = (MovingBlock) target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition =
            Handles.PositionHandle(block.endPoint, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(block, "Change Look At Target Position");
            block.endPoint = newTargetPosition;
        }
    }
}
