// Copyright 2020 Alejandro Villalba Avila
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


/// <summary>
/// It extends the ShadowCaster2D class in order to be able to modify some private data members.
/// </summary>
public static class ShadowCaster2DExtensions
{
    /// <summary>
    /// Replaces the path that defines the shape of the shadow caster.
    /// </summary>
    /// <remarks>
    /// Calling this method will change the shape but not the mesh of the shadow caster. Call SetPathHash afterwards.
    /// </remarks>
    /// <param name="shadowCaster">The object to modify.</param>
    /// <param name="path">The new path to define the shape of the shadow caster.</param>
    public static void SetPath(this UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster, Vector3[] path)
    {
        FieldInfo shapeField = typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePath",
                                                               BindingFlags.NonPublic |
                                                               BindingFlags.Instance);
        shapeField.SetValue(shadowCaster, path);
    }

    /// <summary>
    /// Replaces the hash key of the shadow caster, which produces an internal data rebuild.
    /// </summary>
    /// <remarks>
    /// A change in the shape of the shadow caster will not block the light, it has to be rebuilt using this function.
    /// </remarks>
    /// <param name="shadowCaster">The object to modify.</param>
    /// <param name="hash">The new hash key to store. It must be different from the previous key to produce the rebuild. You can use a random number.</param>
    public static void SetPathHash(this UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster, int hash)
    {
        FieldInfo hashField = typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePathHash",
                                                              BindingFlags.NonPublic |
                                                              BindingFlags.Instance);
        hashField.SetValue(shadowCaster, hash);
    }
}

/// <summary>
/// It provides a way to automatically generate shadow casters that cover the shapes of composite colliders.
/// </summary>
/// <remarks>
/// Specially recommended for tilemaps, as there is no built-in tool that does this job at the moment.
/// </remarks>
public class ShadowCaster2DGenerator
{

#if UNITY_EDITOR
 
    [UnityEditor.MenuItem("Generate Shadow Casters", menuItem = "Tools/Generate Shadow Casters")]
    public static void GenerateShadowCasters()
    {
        CompositeCollider2D[] colliders = GameObject.FindObjectsOfType<CompositeCollider2D>();
 
        for(int i = 0; i < colliders.Length; ++i)
        {
            GenerateTilemapShadowCastersInEditor(colliders[i], false);
        }
    }
 
    [UnityEditor.MenuItem("Generate Shadow Casters (Self Shadows)", menuItem = "Tools/Generate Shadow Casters (Self Shadows)")]
    public static void GenerateShadowCastersSelfShadows()
    {
        CompositeCollider2D[] colliders = GameObject.FindObjectsOfType<CompositeCollider2D>();
 
        for (int i = 0; i < colliders.Length; ++i)
        {
            GenerateTilemapShadowCastersInEditor(colliders[i], true);
        }
    }
 
    /// <summary>
    /// Given a Composite Collider 2D, it replaces existing Shadow Caster 2Ds (children) with new Shadow Caster 2D objects whose
    /// shapes coincide with the paths of the collider.
    /// </summary>
    /// <remarks>
    /// It is recommended that the object that contains the collider component has a Composite Shadow Caster 2D too.
    /// It is recommended to call this method in editor only.
    /// </remarks>
    /// <param name="collider">The collider which will be the parent of the new shadow casters.</param>
    /// <param name="selfShadows">Whether the shadow casters will have the Self Shadows option enabled..</param>
    public static void GenerateTilemapShadowCastersInEditor(CompositeCollider2D collider, bool selfShadows)
    {
        GenerateTilemapShadowCasters(collider, selfShadows);
 
        UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
    }
 
#endif

    /// <summary>
    /// Given a Composite Collider 2D, it replaces existing Shadow Caster 2Ds (children) with new Shadow Caster 2D objects whose
    /// shapes coincide with the paths of the collider.
    /// </summary>
    /// <remarks>
    /// It is recommended that the object that contains the collider component has a Composite Shadow Caster 2D too.
    /// It is recommended to call this method in editor only.
    /// </remarks>
    /// <param name="collider">The collider which will be the parent of the new shadow casters.</param>
    /// <param name="selfShadows">Whether the shadow casters will have the Self Shadows option enabled..</param>
    public static void GenerateTilemapShadowCasters(CompositeCollider2D collider, bool selfShadows)
    {
        // First, it destroys the existing shadow casters
        UnityEngine.Rendering.Universal.ShadowCaster2D[] existingShadowCasters = collider.GetComponentsInChildren<UnityEngine.Rendering.Universal.ShadowCaster2D>();

        for (int i = 0; i < existingShadowCasters.Length; ++i)
        {
            if (existingShadowCasters[i].transform.parent != collider.transform)
            {
                continue;
            }

            GameObject.DestroyImmediate(existingShadowCasters[i].gameObject);
        }

        // Then it creates the new shadow casters, based on the paths of the composite collider
        int pathCount = collider.pathCount;
        List<Vector2> pointsInPath = new List<Vector2>();
        List<Vector3> pointsInPath3D = new List<Vector3>();

        for (int i = 0; i < pathCount; ++i)
        {
            collider.GetPath(i, pointsInPath);

            GameObject newShadowCaster = new GameObject("ShadowCaster2D");
            newShadowCaster.isStatic = true;
            newShadowCaster.transform.SetParent(collider.transform, false);

            for (int j = 0; j < pointsInPath.Count; ++j)
            {
                pointsInPath3D.Add(pointsInPath[j]);
            }

            UnityEngine.Rendering.Universal.ShadowCaster2D component = newShadowCaster.AddComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
            component.SetPath(pointsInPath3D.ToArray());
            component.SetPathHash(Random.Range(int.MinValue, int.MaxValue)); // The hashing function GetShapePathHash could be copied from the LightUtility class
            component.selfShadows = selfShadows;
            component.Update();

            pointsInPath.Clear();
            pointsInPath3D.Clear();
        }
    }
}