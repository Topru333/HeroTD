#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(RoadTile))]
public class RoadTileEditor : MapTileEditor
{

}
#endif