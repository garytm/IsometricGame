using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /*This will be the target the camera should follow*/
    public Transform target;
    /*A float to used to adjust the smoothness of the camera snapping to target*/
    public float smoothSpeed = 0.25f;
    /*A vector for offsetting the camera*/
    public Vector3 offset;
    /*A boolean to check if the camera should rotate or not*/
    public bool rotate;
    /*A float for the rotation speed*/
    public float rotationSpeed;
    /*Using a late update makes the camera motion smoother*/
    void Start()
    {
    }
    void LateUpdate()
    {
        if (rotate == true)
        {
            Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = cameraTurnAngle * offset;
            transform.position = target.position + offset;
        }
        transform.position = target.position + offset;
    }
}
