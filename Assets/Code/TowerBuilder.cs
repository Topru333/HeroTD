using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class TowerBuilder : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> TowersToBuild { get; set; } = new List<GameObject>();
    [field: SerializeField] public TowerGrid Grid { get; set; }

    private bool build_mode = false;
    private Tower tower_to_build = null;
    private List<GameObject> towers_on_map;

    public void Update()
    {
        if (build_mode)
        {
            tower_to_build.transform.position = Grid.PointToTile(OverMousePosition,tower_to_build.TileRadius);
            if (Input.GetMouseButtonDown(1))
            {
                CancelBuild();
                return;
            }

            Vector3Int pos = MouseTilePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (Grid.CanBuild)
                {
                    FinishBuild();
                    return;
                } else
                {
                    //CancelBuild();
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
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {

    }
#endif

    public void Build(int index)
    {
        tower_to_build = Instantiate(TowersToBuild[index]).GetComponent<Tower>();
        //tower_zone = tower_to_build.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        tower_to_build.transform.position = OverMousePosition;
        build_mode = true;
    }

    private void CancelBuild()
    {
        build_mode = false;
        GameObject.Destroy(tower_to_build.gameObject);
        Grid.ClearMarks();
    }

    private void FinishBuild()
    {
        build_mode = false;
        tower_to_build.GetComponent<Tower>().enabled = true;
        towers_on_map.Add(tower_to_build.gameObject);
        tower_to_build.transform.position = Grid.PointToTile(OverMousePosition, tower_to_build.TileRadius, true);
        Grid.ClearMarks();
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
}

#if UNITY_EDITOR
[CustomEditor(typeof(TowerBuilder))]
public class TowerBuilderEditor : Editor
{

    public TowerBuilder builder { get { return (target as TowerBuilder); } }
    public VisionType currentVision = VisionType.Standard;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        currentVision = (VisionType)EditorGUILayout.EnumPopup("View: ",currentVision);
        EditorGUILayout.Space();
        switch(currentVision)
        {
            case VisionType.Standard:
            {
                    base.OnInspectorGUI();
                    if (GUILayout.Button("Rebuild Grid"))
                        builder.Grid.RebuildGridComponent();
                    break;
            }
            case VisionType.TowerList: 
            {
                    InspectorUtils.ShowTowerList(builder.TowersToBuild);
                    break;
            }
        }
        EditorGUILayout.Space();
    }

    

    public enum VisionType
    {
        Standard,
        TowerList
    }
}
#endif