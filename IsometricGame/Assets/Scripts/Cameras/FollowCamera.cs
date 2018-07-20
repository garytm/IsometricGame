using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /*This will be the target the camera should follow*/
    public Transform target;
    /*A float to used to adjust the smoothness of the camera snapping to target*/
    public float smoothSpeed = 0.0f;

    void LateUpdate()
    {
        transform.position = target.position;
    }
}
