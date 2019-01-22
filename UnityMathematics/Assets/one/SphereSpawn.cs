using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    private GameObject mySphere;

    [SerializeField] private float f_sphereMagnitudeX;
    [SerializeField] private float f_sphereMagnitudeY;
    [SerializeField] private float f_sphereFrequncy;


    private GameObject capsule;

    private Camera mainCamera;

    private float f_buttonDownTime;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        capsule = GameObject.Find("Capsule");
    }

    void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            if(mySphere != null)
            {
                Destroy(mySphere);
                mySphere = null;
            }

            mySphere = CreateSphere(Input.mousePosition);
            f_buttonDownTime = Time.time;
        }

        if(mySphere != null)
        {
            Bounce();
        }
	}

    private GameObject CreateSphere(Vector3 mousePos)
    {
        GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 screenPos = mainCamera.WorldToScreenPoint(capsule.transform.position);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, screenPos.z));
        sp.transform.position = worldPos;

        return sp;
    }

    private void Bounce()
    {
        mySphere.transform.position = new Vector3(
            mySphere.transform.position.x + (capsule.transform.position.x - mySphere.transform.position.x) * Time.deltaTime * f_sphereMagnitudeX,
            Mathf.Abs(Mathf.Sin((Time.time - f_buttonDownTime) * (Mathf.PI * 2f) * f_sphereFrequncy) * f_sphereMagnitudeY),
            0
            );
    }
}
