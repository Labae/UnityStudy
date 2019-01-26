using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPath : MonoBehaviour
{
    // waypoints
    public List<Transform> wayPoints = new List<Transform>();

    public bool isLine = true;
    public bool isCurve = false;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        AutoManageBetweenLineToCurve();
    }

    private void AutoManageBetweenLineToCurve()
    {
        if(isLine == true)
        {
            isCurve = false;
        }

        if(isCurve == true)
        {
            isLine = false;
        }

        if(isLine == true && isCurve == true)
        {
            Debug.LogError("WayPointPath isLine and isCurve are true");
        }

        if (isLine == false && isCurve == false)
        {
            Debug.LogError("WayPointPath isLine and isCurve are false");
        }
    }
}
