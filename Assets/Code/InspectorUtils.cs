#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class InspectorUtils 
{
    public static void ShowTowerList(List<GameObject> towers)
    {
        foreach (GameObject gameObject in towers)
        {
            Tower tower = gameObject.GetComponent<Tower>();
            Rect r = EditorGUILayout.BeginHorizontal("Button");
            if (GUI.Button(r, GUIContent.none))
                EditorGUIUtility.PingObject(gameObject);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new StringBuilder().Append("Damage: ").Append(tower.Damage).ToString());
            EditorGUILayout.LabelField(new StringBuilder().Append("Radius: ").Append(tower.Radius).ToString());
            EditorGUILayout.LabelField(new StringBuilder().Append("Attack rate: ").Append(tower.AttackRate).ToString());
            EditorGUILayout.LabelField(new StringBuilder().Append("Price: ").Append(tower.Price).ToString());
            EditorGUILayout.LabelField(new StringBuilder().Append("Fill radius: x - ").Append(tower.TileRadius.x).Append("| y - ").Append(tower.TileRadius.y).ToString());
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            GUILayout.Label(AssetPreview.GetAssetPreview(gameObject.GetComponent<SpriteRenderer>().sprite.texture));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

    }
}
#endif
