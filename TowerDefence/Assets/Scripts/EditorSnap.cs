using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(WayPoint))]
public class EditorSnap : MonoBehaviour
{
    private WayPoint _wayPoint;
    
    private void Awake()
    {
        _wayPoint = GetComponent<WayPoint>();
    }

    void Update ()
    {
        SnapToGrid();

        UpdateLabel();
    }

    private void SnapToGrid()
    {
        Vector2 gridPos = _wayPoint.GetGridPos();

        transform.position = new Vector3(gridPos.x, 0.0f, gridPos.y);
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        StringBuilder str_Pos = new StringBuilder();

        int gridSize = _wayPoint.GetGridSize();
        Vector2 gridPos = _wayPoint.GetGridPos();

        str_Pos.Append(gridPos.x / gridSize);
        str_Pos.Append(", ");
        str_Pos.Append(gridPos.y / gridSize);

        textMesh.text = str_Pos.ToString();
        gameObject.name = str_Pos.ToString();
    }
}
