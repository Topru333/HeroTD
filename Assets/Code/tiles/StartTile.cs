﻿#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{
    public class StartTile : MapTile
    {
        const string TILE_NAME = "Start";


#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tile/" + TILE_NAME + " Tile")]
        public static void CreateMapTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save " + TILE_NAME + " Tile", "New " + TILE_NAME + " Tile", "asset", "Save " + TILE_NAME + " Tile", AssetDatabase.GetAssetPath(Selection.activeObject));
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<StartTile>(), path);
        }
#endif
    }

}
