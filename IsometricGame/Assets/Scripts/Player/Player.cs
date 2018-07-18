using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*Creating a reference to the animator attached to the player character which 
     * allows the animations to be controlled from this class*/
    Animator animator;
    /*A float used to influence the players speed*/
    public float walkSpeed;
    public float runSpeed;
    /*A float for slower player movements (backing up etc.)*/
    public float slowSpeed;
    float moveHorizontal;
    float moveVertical;

    float canJump = 0.0f;
    Vector3 movement;
    /*The rigid body that will be used to manipulate the player*/
    Rigidbody rigid;
    /*An enumerator to hold the states available to the player*/
    public enum PlayerState { Idle, Walking, Running, Jumping }
    public PlayerState playerState;

    void Start()
    {
        /*The initial dog state should be idle*/
        playerState = PlayerState.Idle;
        /*A reference to the rigidbody component*/
        rigid = GetComponent<Rigidbody>();
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateStates();
        /*Ensuring movement works per-frame*/
        Movement();
    }
    void UpdateStates()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isRunning", false);
                break;
            case PlayerState.Walking:
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isRunning", false);
                /*Allows the horizontal and vertical axis to be found from world space*/
                moveHorizontal = Input.GetAxisRaw("Horizontal");
                moveVertical = Input.GetAxisRaw("Vertical");
                movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                /*Slerps the players rotation based on the look rotation and their current movement*/
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.10f);
                transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
                break;
            case PlayerState.Running:
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isWalking", false);
                /*Allows the horizontal and vertical axis to be found from world space*/
                moveHorizontal = Input.GetAxisRaw("Horizontal");
                moveVertical = Input.GetAxisRaw("Vertical");
                movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                /*Slerps the players rotation based on the look rotation and their current movement*/
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.10f);
                transform.Translate(movement * runSpeed * Time.deltaTime, Space.World);
                break;
            case PlayerState.Jumping:
                animator.SetBool("isJumping", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                rigid.velocity = new Vector3(0, 3.5f, 0);
                canJump = Time.time + 1.0f;
                break;
        }
    }
    void Movement()
    {
        playerState = PlayerState.Idle;
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A))
        || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))
        {
            playerState = PlayerState.Walking;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerState = PlayerState.Running;
            }
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > canJump)
        {
            playerState = PlayerState.Jumping;
        }
    }

    IEnumerator WaitTimer()
    {
            yield return new WaitForSeconds(2.0f);
    }
}
