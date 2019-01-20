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
        int gridSize = _wayPoint.GetGridSize();

        transform.position = new Vector3 (
            _wayPoint.GetGridPos().x * gridSize,
            0.0f, 
            _wayPoint.GetGridPos().y * gridSize
            );
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        StringBuilder str_Pos = new StringBuilder();

        // int gridSize = _wayPoint.GetGridSize(); -> never used
        Vector2 gridPos = _wayPoint.GetGridPos();

        str_Pos.Append(gridPos.x);
        str_Pos.Append(", ");
        str_Pos.Append(gridPos.y);

        textMesh.text = str_Pos.ToString();
        gameObject.name = str_Pos.ToString();
    }
}
