using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPointPath))]
public class WatPointPathEditor : Editor
{
    private WayPointPath _wayPointPath;

    private void OnSceneGUI()
    {
        DrawPath();
    }

    private void DrawPath()
    {
        _wayPointPath = (WayPointPath)target;

        int wayPointPathCount = _wayPointPath.wayPoints.Count;
        Vector3[] wayPointVector = GetWayPointsToVector3(wayPointPathCount);

        Handles.color = Color.red;
        if (_wayPointPath.isLine == true)
        {
            DrawPathToLine(wayPointPathCount, wayPointVector);
        }

        if(_wayPointPath.isCurve == true)
        {
            for (int i = 0; i < wayPointPathCount - 3; i++)
            {
                Handles.DrawBezier(wayPointVector[i], wayPointVector[i + 1],
                    wayPointVector[i + 2], wayPointVector[i + 3],
                    Color.red, null, 2f);
            }
        }
    }

    private Vector3[] GetWayPointsToVector3(int wayPointPathCount)
    {
        Vector3[] wayPointVector = new Vector3[wayPointPathCount];
        for (int i = 0; i < wayPointPathCount; i++)
        {
            wayPointVector[i] = _wayPointPath.wayPoints[i].position;
        }

        return wayPointVector;
    }

    private void DrawPathToLine(int wayPointPathCount, Vector3[] wayPointVector)
    {
        for (int element = 0; element < wayPointPathCount - 1; element++)
        {
            Handles.DrawLine(wayPointVector[element], wayPointVector[element + 1]);
        }

        Handles.DrawLine(wayPointVector[wayPointPathCount - 1], wayPointVector[0]);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _wayPointPath = (WayPointPath)target;

        if (GUILayout.Button("Get Child"))
        {
            _wayPointPath.wayPoints.Clear();

            int wayPointCount = _wayPointPath.transform.childCount;

            for (int element = 0; element < wayPointCount; element++)
            {
                _wayPointPath.wayPoints.Add(_wayPointPath.transform.GetChild(element));
            }
        }

        if (GUILayout.Button("Auto Rename numberic"))
        {
            int numberic = 0;

            foreach (var wayPoint in _wayPointPath.wayPoints)
            {
                wayPoint.name = "WayPoint" + numberic;
                numberic++;
            }
        }
    }
}
