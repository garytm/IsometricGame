using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public int zoomAmount = 0;
    public int normal = 60;
    public float smoothAmount = 0.1f;

    public bool isZoomed;

	void Start ()
    {
        isZoomed = false;
    }

	void Update ()
    {
		if (isZoomed)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoomAmount, Time.deltaTime * smoothAmount);
        }
        else
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, normal, Time.deltaTime * smoothAmount);
        }
	}
}
