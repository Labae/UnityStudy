using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyMovement))]
public class EnemyEditor : Editor
{
    private EnemyMovement enemyMovement;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        enemyMovement = (EnemyMovement)target;
        
        if (GUILayout.Button("Get World And WayPoints"))
        {
            GameObject World = enemyMovement.World;

            if (World == null)
            {
                World = GameObject.Find("World");

                enemyMovement.World = World;
            }

            enemyMovement.Path.Clear();

            int childCount = World.transform.childCount;
            for (int element = 0; element < childCount; element++)
            {
                enemyMovement.Path.Add(World.transform.GetChild(element).GetComponent<WayPoint>());
            }
        }
    }
}
