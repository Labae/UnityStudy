using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[ExecuteInEditMode]
[SelectionBase]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] private float f_gridSize = 10.0f;

    private TextMesh textMesh;
    private StringBuilder str_Pos;
    
    void Update ()
    {
        transform.position = CalcSnapPosition();
    }

    private Vector3 CalcSnapPosition()
    {
        Vector3 snapPos;

        snapPos.x = Mathf.RoundToInt(transform.position.x / f_gridSize) * f_gridSize;
        snapPos.y = 0.0f;
        snapPos.z = Mathf.RoundToInt(transform.position.z / f_gridSize) * f_gridSize;

        textMesh = GetComponentInChildren<TextMesh>();
        str_Pos = new StringBuilder();

        str_Pos.Append(snapPos.x / f_gridSize);
        str_Pos.Append(", ");
        str_Pos.Append(snapPos.z / f_gridSize);

        textMesh.text = str_Pos.ToString();
        gameObject.name = str_Pos.ToString();

        return snapPos;
    }
}
