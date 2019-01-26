using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class three : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    public float scrollSpeed = 200.0f;
    public Transform target;

    [System.Serializable]
    public class SphericalCoordinates
    {
        // 반지름
        public float radius;
        // 방위각
        public float azimuth;
        // 앙각
        public float elevation;

        public float Radius
        {
            get { return radius; }
            set
            {
                radius = Mathf.Clamp(value, m_minRadius, m_maxRadius);
            }
        }

        public float Azimuth
        {
            get { return azimuth; }
            set
            {
                azimuth = Mathf.Repeat(value, m_maxAzimuth - m_minAzimuth);
            }
        }

        public float Elevation
        {
            get { return elevation; }
            set
            {
                elevation = Mathf.Clamp(value, m_minElevation, m_maxElevation);
            }
        }

        public float m_minRadius = 3.0f;
        public float m_maxRadius = 20.0f;

        public float minAzimuth = 0.0f;
        private float m_minAzimuth;

        public float maxAzimuth = 360.0f;
        private float m_maxAzimuth;

        public float minElevation = 0.0f;
        private float m_minElevation;

        public float maxElevation = 90.0f;
        private float m_maxElevation;



        // cartesianCoordinate = 데카르트 좌표계
        public SphericalCoordinates(Vector3 cartesianCoordinate)
        {
            // change radian
            m_minAzimuth = Mathf.Deg2Rad * minAzimuth;
            m_maxAzimuth = Mathf.Deg2Rad * maxAzimuth;
            m_minElevation = Mathf.Deg2Rad * minElevation;
            m_maxElevation = Mathf.Deg2Rad * maxElevation;

            Radius = cartesianCoordinate.magnitude;
            Azimuth = Mathf.Atan2(cartesianCoordinate.z, cartesianCoordinate.x);
            Elevation = Mathf.Asin(cartesianCoordinate.y / Radius);
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

    public SphericalCoordinates sphericalCoordinates;

    private List<Vector3> triangleVertices = new List<Vector3>();

    private float f_keyboardHorizontal;
    private float f_keyboardVertical;

    private float f_mouseHorizontal;
    private float f_mouseVertical;

    private float f_horizontal;
    private float f_vertical;

    private float f_scrollWheel;

    private void Start()
    {
        sphericalCoordinates = new SphericalCoordinates(transform.position);
        transform.position = sphericalCoordinates.toCartesian + target.position;

        Mesh mesh = target.gameObject.GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            if(triangleVertices.Count < 3)
            {
                triangleVertices.Add(mesh.vertices[i]);
            }
        }
    }

    private void Update()
    {
        DrawCameraLine();
        ThridPersonCameraProcess();
    }

    private void DrawCameraLine()
    {
        Debug.DrawLine(target.position, target.transform.forward * 2f, Color.blue);

        Vector3 cameraPoint = transform.position + transform.forward * 5f;

        Vector3 triangle_Edge1 = triangleVertices[1] - triangleVertices[0];
        Vector3 triangle_Edge2 = triangleVertices[2] - triangleVertices[1];
        Vector3 triangle_Edge3 = triangleVertices[0] - triangleVertices[2];

        Vector3 camToEdge1 = cameraPoint - triangleVertices[1];
        Vector3 camToEdge2 = cameraPoint - triangleVertices[2];
        Vector3 camToEdge3 = cameraPoint - triangleVertices[0];

        Vector3 crossPoint1 = Vector3.Cross(triangle_Edge1, camToEdge1);
        Vector3 crossPoint2 = Vector3.Cross(triangle_Edge2, camToEdge2);
        Vector3 crossPoint3 = Vector3.Cross(triangle_Edge3, camToEdge3);

        if (Vector3.Dot(crossPoint1, crossPoint2) > 0 && Vector3.Dot(crossPoint1, crossPoint3) > 0)
        {
            Debug.DrawLine(transform.position, cameraPoint, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, cameraPoint, Color.green);
        }
    }

    private void ThridPersonCameraProcess()
    {
        MouseClick();
        CalcHorizontalAndVertical();
        Rotate();
        Zoom();
        transform.LookAt(target);
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
                              sphericalCoordinates.Rotate(f_horizontal * rotateSpeed * Time.deltaTime,
                              f_vertical * rotateSpeed * Time.deltaTime).toCartesian
                              + target.position;
        }
    }

    private void Zoom()
    {
        f_scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (f_scrollWheel * f_scrollWheel > Mathf.Epsilon)
        {
            transform.position =
                              sphericalCoordinates.TranslateRadius(f_scrollWheel * scrollSpeed * Time.deltaTime).toCartesian
                              + target.position;
        }
    }
}
