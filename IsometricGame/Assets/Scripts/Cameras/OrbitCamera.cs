using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{ 
    public Transform cameraFocus;
    public float rotateSpeed;

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(cameraFocus.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(cameraFocus.position, -Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
