using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /*This will be the target the camera should follow*/
    public Transform target;
    /*A vector for the offset between the camera and the player*/
    Vector3 offset;
    /*A float to used to adjust the smoothness of the camera snapping to target
     Using Range creates a slider in the Inspector*/
    [Range(0.01f, 1.0f)]
    public float smoothSpeed = 0.5f;

    public bool lookAtTarget = false;

    void Start()
    {
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        /*Setting the cameras position to be the player + the offset*/
        Vector3 newPosition = target.position + offset;
        /*Interpolating between the current and new camera positions using Slerp to give a smooth rotation*/
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothSpeed);
        
        if (lookAtTarget == true)
        {
            transform.LookAt(target);
        }
    }
}
