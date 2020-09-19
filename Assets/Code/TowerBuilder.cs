using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class TowerBuilder : MonoBehaviour
{
    [field: SerializeField] public Tilemap LogicTileMap { get; set; } = null;
    [field: SerializeField] public List<GameObject> TowersToBuild { get; set; } = new List<GameObject>();
    [SerializeField] private TowerGrid _grid;


    private bool build_mode = false;
    private GameObject tower_to_build = null;
    private SpriteRenderer tower_zone = null;
    private List<GameObject> towers_on_map;


    public void Update()
    {
        if (build_mode)
        {
            tower_to_build.transform.position = _grid.PointToTile(OverMousePosition);

            

            if (Input.GetMouseButtonDown(1))
            {
                CancelBuild();
                return;
            }

            Vector3Int pos = MouseTilePosition;
            TileBase tile = LogicTileMap.GetTile(pos);

            if (ValidTile(tile))
            {
                tower_zone.color = _grid.ColorAvailable;
                if (Input.GetMouseButtonDown(0))
                {

                    build_mode = false;
                    tower_to_build.GetComponent<Tower>().enabled = true;
                    towers_on_map.Add(tower_to_build);
                    tower_zone.enabled = false;
                    return;
                }
            }
            else
            {
                tower_zone.color = _grid.ColorNotAvailable;
                if (Input.GetMouseButtonDown(0))
                {
                    CancelBuild();
                }
            }
        }
    }

    public void Start()
    {
        towers_on_map = new List<GameObject>();
        foreach(GameObject tower in TowersToBuild)
        {
            if (tower.GetComponent<Tower>() == null)
            {
                throw new MissingComponentException("In tower to build list one or more object without <Tower> component!");
            }
        }
        Debug.Log("MAP SIZE: " + LogicTileMap.size.x + " " + LogicTileMap.size.y);
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        
        if (_grid != null)
        {
            _grid.DrawGizmos();
        }
        
    }
#endif

    public void Build(int index)
    {
        tower_to_build = Instantiate(TowersToBuild[index]);
        tower_zone = tower_to_build.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        tower_to_build.transform.position = OverMousePosition;
        build_mode = true;
    }

    public void CancelBuild()
    {
        build_mode = false;
        GameObject.Destroy(tower_to_build);
    }

    private Vector3 OverMousePosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private Vector3Int MouseTilePosition
    {
        get
        {
            Vector3Int vector = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            vector.z = 0;
            return vector;
        }
    }

    // Check tile is valid to build
    private bool ValidTile(TileBase tile)
    {
        return tile != null && !(tile is FinishTile || tile is StartTile || tile is RoadTile);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TowerBuilder))]
public class TowerBuilderEditor : Editor
{
    public TowerBuilder builder { get { return (target as TowerBuilder); } }
    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.Space();   
        //builder.TowersToBuild = EditorGUILayout.ObjectField(builder.TowersToBuild, typeof(List<GameObject>), true);
        base.OnInspectorGUI();
        EditorGUILayout.Space();
    }
}
#endif