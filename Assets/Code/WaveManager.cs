using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct Wave
{
    public GameObject mob;
    public int count;
}

public class WaveManager : MonoBehaviour
{
    private Tilemap logicTileMap;
    public List<Vector2> road_path = new List<Vector2>();
    public List<Wave> waves = new List<Wave>();

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
           
        if (road_path.Count > 1)
        {
            for(int i = 0; i < road_path.Count; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(road_path[i], 0.3f);
                if (road_path.Count != i + 1)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(road_path[i], road_path[i+1]);
                }
            }

        }
        
    }
#endif

    public void StartWave()
    {
        if (waves.Count > 0)
        {
            StartCoroutine(Spawn(1f, waves[0].count, waves[0].mob, road_path));
            waves.RemoveAt(0);
        }
    }

    public void Start()
    {
        logicTileMap = GridHelper.instance.LogicTileMap;
        Vector3Int start = Vector3Int.zero;
        foreach (Vector3Int position in logicTileMap.cellBounds.allPositionsWithin)
        {
            if (logicTileMap.HasTile(position) && logicTileMap.GetTile(position) is StartTile)
            {
                start = position;
            }
        }
        if(start != Vector3Int.zero)
        {
            road_path = getPath(start, logicTileMap);
        }
    }

    IEnumerator Spawn(float delay, int count, GameObject prefab, List<Vector2> mobPath)
    {
        yield return new WaitForSeconds(delay);

        GameObject new_mob = Instantiate(prefab, new Vector3(mobPath[0].x, mobPath[0].y, -0.1f), Quaternion.identity);
        new_mob.GetComponent<Mob>().Path = mobPath;
        if (--count>0)
        { 
            StartCoroutine(Spawn(delay, count, prefab, mobPath));
        }
    }

    private List<Vector2> getPath(Vector3Int start, Tilemap tilemap)
    {
        List<Vector3Int> pathInt = new List<Vector3Int>();
        pathInt.Add(start);

        Vector3Int[] directions = { Vector3Int.left, Vector3Int.right, Vector3Int.up, Vector3Int.down };

        Vector3Int checkPoint;
        TileBase checkTile;

        for (bool found = true; found;)
        {
            found = false;
            foreach (Vector3Int direction in directions)
            {
                checkPoint = pathInt[pathInt.Count-1] + direction;
                if (!tilemap.HasTile(checkPoint)) continue;
                checkTile = tilemap.GetTile(checkPoint);
                
                if (!(checkTile is GrassTile) && (pathInt.Count == 1 || pathInt[pathInt.Count-2] != checkPoint))
                {
                    pathInt.Add(checkPoint);
                    found = true;
                    break;
                }
            }
        }

        for (int i = 1, j = 2; j < pathInt.Count; i++, j++ )
        { 
            if (((Vector3)pathInt[i] - pathInt[i - 1]).normalized == (pathInt[j] - pathInt[i]))
            {
                pathInt.RemoveAt(i);
                i--;
                j--;
            }
        }

        List<Vector2> pathFloat = new List<Vector2>();
        pathInt.ForEach(point => pathFloat.Add(tilemap.CellToWorld(point) + tilemap.tileAnchor));

        return pathFloat;
    }
}
