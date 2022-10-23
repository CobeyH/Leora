using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightBuilder))]
public class LightBuilderEditor : Editor
{
    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LightBuilder builder = target as LightBuilder;
        if (builder.lightData.area == AreaOfEffect.Directional)
            builder.lightData.canRotate = EditorGUILayout.Toggle("Is Rotatable", builder.lightData.canRotate);

    }
}