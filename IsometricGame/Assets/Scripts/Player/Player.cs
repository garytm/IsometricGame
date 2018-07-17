using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*Creating a reference to the animator attached to the player character which 
     * allows the animations to be controlled from this class*/
    Animator animator;
    /*A float used to influence the players speed*/
    public float moveSpeed;
    /*A float for slower player movements (backing up etc.)*/
    public float slowSpeed;
    float moveHorizontal;
    float moveVertical;
    /*A float to time animations*/
    float animationTimer = 2.0f;
    /*A float to check if the player can jump*/
    float canJump = 0.0f;
    /*A boolean to check if the player is currently jumping*/
    bool isJumping;
    Vector3 movement;
    /*The rigid body that will be used to manipulate the player*/
    Rigidbody rigid;
    /*An enumerator to hold the states available to the player*/
    public enum PlayerState { Grounded, Walking, Running, Jumping, Falling, Swimming }
    public PlayerState playerState;

    void Start()
    {
        /*The initial dog state should be idle*/
        playerState = PlayerState.Grounded;
        /*A reference to the rigidbody component*/
        rigid = GetComponent<Rigidbody>();
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        /*Ensuring movement works per-frame*/
        Movement();
        UpdateStates();
    }

    void UpdateStates()
    {
        switch (playerState)
        {
            case PlayerState.Grounded:
                animator.SetBool("isWalking", false);
                animator.SetInteger("isJumping", 0);
                break;
            case PlayerState.Walking:
                animator.SetBool("isWalking", true);
                break;
            case PlayerState.Running:
                animator.SetBool("isRunning", true);
                break;
            case PlayerState.Jumping:
                animator.SetInteger("isJumping", 1);
                rigid.velocity = new Vector3(0, 3.5f, 0);
                canJump = Time.time + 2.0f;
                break;
            case PlayerState.Falling:
                animator.SetBool("isFalling", true);
                break;
            case PlayerState.Swimming:
                animator.SetBool("isSwimming", true);
                break;
        }
    }
    void Movement()
    {
        playerState = PlayerState.Grounded;

        if (Input.GetKey(KeyCode.W))
        {
            playerState = PlayerState.Walking;
        }
         if (Input.GetKey(KeyCode.A))
        {
            playerState = PlayerState.Walking;
        }
         if (Input.GetKey(KeyCode.S))
        {
            playerState = PlayerState.Walking;
        }
         if (Input.GetKey(KeyCode.D))
        {
            playerState = PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > canJump)
        {
            playerState = PlayerState.Jumping;
        }
        /*Allows the horizontal and vertical axis to be found from world space*/
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        /*Slerps the players rotation based on the look rotation and their current movement*/
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.10f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        }
        while (animation.isPlaying);
    }
}
