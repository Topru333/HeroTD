#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(MapTile))]
public class MapTileEditor : Editor
{
    public MapTile tile { get { return (target as MapTile); } }

    GUILayoutOption[] options = { GUILayout.Height(100), GUILayout.Width(100) };


    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        tile.layer = EditorGUILayout.LayerField("Layer:", tile.layer);
        tile.collider = (Tile.ColliderType)EditorGUILayout.EnumPopup("Collider type:", tile.collider);
        EditorGUILayout.Space();

        tile.tileSprite = (Sprite)EditorGUILayout.ObjectField(tile.tileSprite, typeof(Sprite), false, options);
        
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(tile);
    }
}
#endif