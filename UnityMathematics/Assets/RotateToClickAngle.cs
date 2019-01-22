using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToClickAngle : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float f_rotateSpeed = 5.0f;

    private float f_targetAngle;

    private bool isRotate = false;

    private Camera mainCamera;

    private Vector3 clickPosition;

    private void Start()
    {
        if(targetObject == null)
        {
            targetObject = this.gameObject;
        }

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeforeRotateSet();
        }

        CheckRotateCondition();
    }

    private void BeforeRotateSet()
    {
        isRotate = true;
        clickPosition = Input.mousePosition;
    }

    private void CheckRotateCondition()
    {
        if (isRotate == true)
        {
            myRotate();
        }

        if (targetObject.transform.eulerAngles.z == f_targetAngle)
        {
            isRotate = false;
        }
    }

    private void myRotate()
    {
        f_targetAngle = GetRotatationAngle(clickPosition);
        
        targetObject.transform.eulerAngles = new Vector3(0, 0,
                                   Mathf.MoveTowardsAngle(targetObject.transform.eulerAngles.z,
                                   f_targetAngle,
                                   Time.deltaTime * f_rotateSpeed)
                                   );
    }

    private float GetRotatationAngle(Vector3 mousePos)
    {
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetObject.transform.position);
        Vector3 diff = mousePos - screenPoint;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        
        float finalAngle = angle - 90.0f;

        return finalAngle;
    }
}
