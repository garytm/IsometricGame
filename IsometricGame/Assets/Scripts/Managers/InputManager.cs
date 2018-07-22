//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InputManager : MonoBehaviour
//{
//    public float joystickDeadzone = 0.1f;
//    public float joystickSensitivity = 1.0f;
//    public float mouseSensitivity = 1.0f;
//    public float moveHorizontal;
//    public float moveVertical;

//    Player player;

//	void Start ()
//    {
//        player = FindObjectOfType<Player>();
//	}
	
//	void Update ()
//    {
//        Movement();
//	}

//    void Movement()
//    {
//        moveHorizontal = Input.GetAxis("Horizontal");
//        moveVertical = Input.GetAxis("Vertical");

//        if (moveHorizontal < joystickDeadzone || moveHorizontal > -joystickDeadzone)
//            moveHorizontal = 0;
//        if (moveVertical < joystickDeadzone || moveVertical > -joystickDeadzone)
//            moveVertical = 0;
//        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
//        direction.Normalize();
//        player.Movement();

//    }
//}
