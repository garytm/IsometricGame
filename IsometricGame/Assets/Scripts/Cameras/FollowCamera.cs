using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /*This will be the target the camera should follow*/
    public Transform target;
    /*A float to used to adjust the smoothness of the camera snapping to target*/
    public float smoothSpeed = 0.25f;
    /*A vector for offsetting the camera*/
    public Vector3 offset;
    /*Using a late update makes the camera motion smoother*/
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
