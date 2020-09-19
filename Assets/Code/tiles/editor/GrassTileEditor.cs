#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(GrassTile))]
public class GrassTileEditor : MapTileEditor
{  
    /*
    public override void OnInspectorGUI()
    {
       
        EditorGUI.BeginChangeCheck();
        tile.layer = EditorGUILayout.LayerField("Layer", tile.layer);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical();
            {
                tile.left_up = (Sprite)EditorGUILayout.ObjectField(tile.left_up, typeof(Sprite), false, options);
                tile.left = (Sprite)EditorGUILayout.ObjectField(tile.left, typeof(Sprite), false, options);
                tile.left_down = (Sprite)EditorGUILayout.ObjectField(tile.left_down, typeof(Sprite), false, options);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            {
                tile.up = (Sprite)EditorGUILayout.ObjectField(tile.up, typeof(Sprite), false, options);
                tile.normal = (Sprite)EditorGUILayout.ObjectField(tile.normal, typeof(Sprite), false, options);
                tile.down = (Sprite)EditorGUILayout.ObjectField(tile.down, typeof(Sprite), false, options);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            {
                tile.right_up = (Sprite)EditorGUILayout.ObjectField(tile.right_up, typeof(Sprite), false, options);
                tile.right = (Sprite)EditorGUILayout.ObjectField(tile.right, typeof(Sprite), false, options);
                tile.right_down = (Sprite)EditorGUILayout.ObjectField(tile.right_down, typeof(Sprite), false, options);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(tile);
       
    } */
}
#endif
