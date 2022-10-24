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

            // Light2D light2D = builder.lightEmitter.GetComponent<Light2D>();
            // light2D.pointLightOuterAngle = EditorGUILayout.Slider("Leaf Angle", light2D.pointLightOuterAngle, 5, 360);
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty (builder);
            EditorSceneManager.MarkSceneDirty(builder.gameObject.scene);
        }
    }
}
