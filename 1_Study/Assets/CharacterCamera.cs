using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SphericalCoordinates
{
    private float m_radius;
    private float m_azimuth;
    private float m_elevation;

    [SerializeField] private float m_minRadius = 1.0f;
    [SerializeField] private float m_maxRadius = 10.0f;

    [SerializeField] private float m_degreeMinAzimuth = 0.0f;
    [SerializeField] private float m_degreeMaxAzimuth = 360.0f;

    [SerializeField] private float m_degreeMinElevation = -80.0f;
    [SerializeField] private float m_degreeMaxElevation = 89.0f;

    private float m_radianMinAzimuth;
    private float m_radianMaxAzimuth;

    private float m_radianMinElevation;
    private float m_radianMaxElevation;

    public float Radius
    {
        get
        {
            return m_radius;
        }
        set
        {
            m_radius = Mathf.Clamp(value, m_minRadius, m_maxRadius);
        }
    }

    public float Azimuth
    {
        get
        {
            return m_azimuth;
        }
        set
        {
            m_azimuth = Mathf.Repeat(value, m_radianMaxAzimuth - m_radianMinAzimuth);
        }
    }

    public float Elevation
    {
        get
        {
            return m_elevation;
        }
        set
        {
            m_elevation = Mathf.Clamp(value, m_radianMinElevation, m_radianMaxElevation);
        }
    }

    public SphericalCoordinates(Vector3 cartesianCoordinates)
    {
        m_radianMinAzimuth = m_degreeMinAzimuth * Mathf.Deg2Rad;
        m_radianMaxAzimuth = m_degreeMaxAzimuth * Mathf.Deg2Rad;
        m_radianMinElevation = m_degreeMinElevation * Mathf.Deg2Rad;
        m_radianMaxElevation = m_degreeMaxElevation * Mathf.Deg2Rad;

        Radius = cartesianCoordinates.magnitude;
        Azimuth = Mathf.Atan2(cartesianCoordinates.y, cartesianCoordinates.x);
        Elevation = Mathf.Asin(cartesianCoordinates.y / Radius);
    }

    public Vector3 toCartesian
    {
        get
        {
            float t = Mathf.Cos(Elevation) * Radius;
            return new Vector3(t * Mathf.Cos(Azimuth), Radius * Mathf.Sin(Elevation), t * Mathf.Sin(Azimuth));
        }
    }

    public SphericalCoordinates Rotate(float newAzimuth, float newElevation)
    {
        Azimuth += newAzimuth;
        Elevation += newElevation;

        return this;
    }

    public SphericalCoordinates TranslateRadius(float amount)
    {
        Radius += amount;

        return this;
    }
}

public class CharacterCamera : MonoBehaviour
{
    enum CameraType { First, Thrid };

    [SerializeField]
    private CameraType currentType = CameraType.Thrid;
    private CameraType oldCameraType = CameraType.Thrid;

    [SerializeField] private Transform targetTransform;
    [SerializeField] private float f_thridRotateSpeed = 1.0f;
    [SerializeField] private float f_scrollSpeed = 200.0f;

    [SerializeField]
    private SphericalCoordinates sphericalCoordinates;

    private float f_keyboardHorizontal;
    private float f_keyboardVertical;

    private float f_mouseHorizontal;
    private float f_mouseVertical;

    private float f_horizontal;
    private float f_vertical;

    private float f_scrollWheel;

    [SerializeField] [Range(1f,5f)]
    private float f_moveSpeed = 5.0f;
    private float f_offsetMoveSpeed;
    [SerializeField] [Range(0.1f,1f)]
    private float f_whileMovingRotateSpeed = 1.0f;
    private float f_offsetRotateSpeed;
    
    private bool isArrive = true;
    private bool isCalcMoveSpeed = false;
    private bool isCalcRotateSpeed = false;

    [SerializeField]
    private Transform firstPersonTransform;
    private Vector3 thirdOldPosition;
    
    private void Start()
    {
        sphericalCoordinates = new SphericalCoordinates(transform.position);
        transform.position = sphericalCoordinates.toCartesian + targetTransform.position;
    }

    private void Update()
    {
        ChangeCameraProcess();

        if (currentType == CameraType.Thrid)
        {
            ThridPersonCameraProcess();
        }
    }

    private void ChangeCameraProcess()
    {
        if (oldCameraType != currentType)
        {
            MoveToPosition(currentType);

            if (currentType == CameraType.First)
            {
                RotateToFirstPersonRotation();
            }
        }

        if (Input.GetMouseButtonDown(1) && isArrive == true)
        {
            isCalcMoveSpeed = false;
            isCalcRotateSpeed = false;
            ChangeCameraType();
        }
    }
    
    private void MoveToPosition(CameraType type)
    {
        Vector3 movePosition = GetMovePosition(type);
        
        if (transform.position != movePosition)
        {
            isArrive = false;
            CalcMoveSpeed(movePosition);

            transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime * f_offsetMoveSpeed * f_moveSpeed);
        }
        else
        {
            isArrive = true;
            oldCameraType = currentType;
        }
    }

    private void CalcMoveSpeed(Vector3 movePosition)
    {
        if (isCalcMoveSpeed == false)
        {
            f_offsetMoveSpeed = Vector3.Distance(transform.position, movePosition);

            isCalcMoveSpeed = true;
        }
    }

    // if error => return Vector3.zero
    private Vector3 GetMovePosition(CameraType type)
    {
        Vector3 movePosition;

        if (type == CameraType.First)
        {
            movePosition = firstPersonTransform.position;
            return movePosition;
        }

        if (type == CameraType.Thrid)
        {
            movePosition = thirdOldPosition;
            return movePosition;
        }

        return Vector3.zero;
    }

    private void RotateToFirstPersonRotation()
    {
        CalcRotateSpeed();

        transform.rotation = Quaternion.Lerp(transform.rotation, firstPersonTransform.rotation, Time.deltaTime * f_offsetRotateSpeed * f_whileMovingRotateSpeed);
    }

    private void CalcRotateSpeed()
    {
        if(isCalcRotateSpeed == false)
        {
            f_offsetRotateSpeed = Quaternion.Angle(transform.rotation, firstPersonTransform.rotation);

            isCalcRotateSpeed = true;
        }
    }

    private void ChangeCameraType()
    {
        if (currentType == CameraType.First)
        {
            currentType = CameraType.Thrid;
        }
        else if (currentType == CameraType.Thrid)
        {
            thirdOldPosition = transform.position;
            currentType = CameraType.First;
        }
    }

    private void ThridPersonCameraProcess()
    {
        MouseClick();
        CalcHorizontalAndVertical();
        Rotate();
        Zoom();
        transform.LookAt(targetTransform);
    }

    private void MouseClick()
    {
        bool isAnyMouseButtonClick = Input.GetMouseButton(0) | Input.GetMouseButton(1) | Input.GetMouseButton(2);

        if (isAnyMouseButtonClick == true)
        {
            f_mouseHorizontal = Input.GetAxis("Mouse X");
            f_mouseVertical = Input.GetAxis("Mouse Y");
        }
        else
        {
            f_mouseHorizontal = 0f;
            f_mouseVertical = 0f;
        }
    }

    private void CalcHorizontalAndVertical()
    {
        f_keyboardHorizontal = Input.GetAxis("Horizontal");
        f_keyboardVertical = Input.GetAxis("Vertical");

        if (f_keyboardHorizontal * f_keyboardHorizontal > f_mouseHorizontal * f_mouseHorizontal)
        {
            f_horizontal = f_keyboardHorizontal;
        }
        else
        {
            f_horizontal = f_mouseHorizontal;
        }

        if (f_keyboardVertical * f_keyboardVertical > f_mouseVertical * f_mouseVertical)
        {
            f_vertical = f_keyboardVertical;
        }
        else
        {
            f_vertical = f_mouseVertical;
        }
    }

    private void Rotate()
    {
        if (f_horizontal * f_horizontal > Mathf.Epsilon || f_vertical * f_vertical > Mathf.Epsilon)
        {
            transform.position =
                              sphericalCoordinates.Rotate(f_horizontal * f_thridRotateSpeed * Time.deltaTime,
                              f_vertical * f_thridRotateSpeed * Time.deltaTime).toCartesian
                              + targetTransform.position;
        }
    }

    private void Zoom()
    {
        f_scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (f_scrollWheel * f_scrollWheel > Mathf.Epsilon)
        {
            transform.position =
                              sphericalCoordinates.TranslateRadius(f_scrollWheel * f_scrollSpeed * Time.deltaTime).toCartesian
                              + targetTransform.position;
        }
    }

    private void FirstPersonCameraProcess()
    {
        // todo...
        // 1. FirstPerson Zoom
        // 2. Clamp
        // 3. ...
    }
}
