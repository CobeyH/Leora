using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CustomEditor(typeof (LightBuilder))]
public class LightBuilderEditor : Editor
{
    SerializedProperty canRotate;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LightBuilder builder = target as LightBuilder;

        if (builder.lightData.area == AreaOfEffect.Directional)
        {
            builder.lightData.canRotate =
                EditorGUILayout
                    .Toggle("Is Rotatable", builder.lightData.canRotate);
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty (builder);
            EditorSceneManager.MarkSceneDirty(builder.gameObject.scene);
        }
    }
}
