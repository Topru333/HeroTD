#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{
    public abstract class MapTile : TileBase
    {
        public Sprite tileSprite;
        public int layer = 0;
        public Tile.ColliderType collider = Tile.ColliderType.None;

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            base.GetTileData(location, tileMap, ref tileData);

            tileData.sprite = tileSprite;

            tileData.colliderType = collider;
        }
    }
}


