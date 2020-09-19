using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class TowerGrid 
{
    [field: SerializeField] public Vector3 Offset { get; private set; } = Vector3.zero;
    [field: SerializeField] public Vector3 CellSize { get; private set; } = Vector3.one;
    [field: SerializeField] public Sprite AvailibleSprite { get; private set; }
    [field: SerializeField] public Sprite NotAvailibleSprite { get; private set; }
    
    public bool CanBuild { get; private set; } = false;


#if UNITY_EDITOR
    public void RebuildGridComponent()
    {
        EditorGUIUtility.PingObject(GridHelper.instance.TowersGrid);
        GridHelper.instance.TowersGrid.transform.position = Vector3.zero + Offset;

        Grid grid = GridHelper.instance.TowersGrid.GetComponent<Grid>();
        grid.cellSize = Vector3.one;
        grid.transform.localScale = CellSize;
        grid.cellGap = Vector3.zero;
        grid.cellLayout = Grid.CellLayout.Rectangle;
        grid.cellSwizzle = Grid.CellSwizzle.XYZ;
    }
#endif

    public Vector3 PointToTile(Vector3 point)
    {
        Vector3 result = Vector3.zero;
        Vector3 cell_offset = CellSize * .5f;
        result.x = (int)(point.x / CellSize.x) * CellSize.x + (point.x < 0 ? -cell_offset.x : cell_offset.x) + Offset.x;
        result.y = (int)(point.y / CellSize.y) * CellSize.y + (point.y < 0 ? -cell_offset.y : cell_offset.y) + Offset.x;

        return result;
    }

    public Vector3 PointToTile(Vector3 point, Vector2Int radius, bool finish = false)
    {
        var result = PointToTile(point);
        ClearMarks();
        MarkTiles(result, radius, finish);
        return result;
    }

    public void ClearMarks()
    {
        GridHelper.instance.LayoutTileMap.ClearAllTiles();
    }


    // God hates this function
    public void MarkTiles(Vector3 pos, Vector2Int radius, bool determinant = false)
    {
        var _layoutTileMap = GridHelper.instance.LayoutTileMap;
        var _detTileMap = GridHelper.instance.TowerDeterminant;
        var cellpos = _layoutTileMap.WorldToCell(pos);
        CanBuild = true;
        if (radius == Vector2Int.zero)
        {
            _layoutTileMap.SetTile(cellpos, GetAvailibleTile());
        } else
        {
            List<Vector3Int> tile_positions = new List<Vector3Int>();
            List<TileBase> tiles = new List<TileBase>();
            for (int x = -radius.x; x < radius.x + 1; x++)
            {
                for (int y = -radius.y; y < radius.y + 1; y++)
                {
                    Vector3Int tile_position = new Vector3Int(cellpos.x + x, cellpos.y + y, cellpos.z);
                    tile_positions.Add(tile_position);
                    TileBase logic_tile = GridHelper.instance.LogicTileMap.GetTile(GridHelper.instance.LogicTileMap.WorldToCell(_layoutTileMap.CellToWorld(tile_position)));
                    TileBase det_tile = _detTileMap.GetTile(tile_position);

                    CanBuild = CanBuild && LogicTileValid(logic_tile) && det_tile == null;
                    tiles.Add(GetAvailibleTile(LogicTileValid(logic_tile) && det_tile == null));
                }
            }

            if (determinant)
            {
                _detTileMap.SetTiles(tile_positions.ToArray(), tiles.ToArray());
            } else
            {
                _layoutTileMap.SetTiles(tile_positions.ToArray(), tiles.ToArray());
            }
        }
    }

    private MapTile GetAvailibleTile(bool availible = true)
    {
        MapTile maptile = ScriptableObject.CreateInstance<GrassTile>();
        maptile.tileSprite = availible ? AvailibleSprite : NotAvailibleSprite;
        return maptile;
    }

    private bool LogicTileValid(TileBase tile)
    {
        return tile != null && tile is GrassTile;
    }

}
