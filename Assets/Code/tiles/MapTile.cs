#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{
    public abstract class MapTile : TileBase
    {
        public Sprite tileSprite;
        public int layer;
        public Tile.ColliderType collider;

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            base.GetTileData(location, tileMap, ref tileData);

            tileData.sprite = tileSprite;

            tileData.colliderType = collider;
        }
    }
}


