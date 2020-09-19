using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHelper : MonoBehaviour
{
    public static GridHelper instance;
    [field: SerializeField] public Tilemap LogicTileMap { get; private set; }
    [field: SerializeField] public Tilemap LayoutTileMap { get; private set; }
    [field: SerializeField] public Grid TowersGrid { get; private set; }
    [field: SerializeField] public Tilemap TowerDeterminant { get; private set; }

    public GridHelper()
    {
        instance = this;
    }
}
