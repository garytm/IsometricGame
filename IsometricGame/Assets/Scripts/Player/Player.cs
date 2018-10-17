using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*Creating a reference to the animator attached to the player character which 
     * allows the animations to be controlled from this class*/
    Animator animator;
    SimpleDialogue simpleDialogue;
    /*The rigid body that will be used to manipulate the player*/
    public Rigidbody rigid;
    /*A float used to influence the players speed*/
    public float walkSpeed;
    public float runSpeed;
    /*A float for slower player movements (backing up etc.)*/
    public float slowSpeed;
    float canJump = 0.0f;
    /*An enumerator to hold the states available to the player*/
    public enum State { Idle, Walking, Running, Jumping }
    public State state;
    public string backgroundMusicName;

    public bool canMove = true;

    AudioManager audioManager;
    void Start()
    {
        /*The initial dog state should be idle*/
        state = State.Idle;
        simpleDialogue = FindObjectOfType<SimpleDialogue>();
        /*A reference to the rigidbody component*/
        rigid = GetComponent<Rigidbody>();
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene!");
        }
        audioManager.PlaySound(backgroundMusicName);
    }
    void Update()
    {
        /*Ensuring the movement is updated every frame*/
        Movement();
        /*Ensuring the states are updated every frame*/
        UpdateStates();
    }
    public void Movement()
    {
        state = State.Idle;
        float deadZone = 0.1f;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");
        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction.Normalize();
        if (canMove == true)
        {
            if (moveHorizontal > deadZone || moveHorizontal < -deadZone || moveVertical > deadZone || moveVertical < -deadZone)
            {
                state = State.Walking;
                /*Slerps the players rotation based on the look rotation and their current movement*/
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.10f);
                transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey("joystick button 1"))
                {
                    state = State.Running;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.10f);
                    transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);
                }
            }
            if (jump > deadZone && Time.time > canJump)
            {
                state = State.Jumping;
            }
        }
    }
    /*This method contains switch statements for animation settings when the player is in each state*/
    void UpdateStates()
    {
        switch (state)
        {
            case State.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isRunning", false);
                break;
            case State.Walking:
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isRunning", false); 
                break;
            case State.Running:
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isWalking", false);
                break;
            case State.Jumping:
                animator.SetBool("isJumping", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                rigid.velocity = new Vector3(0, 3.5f, 0);
                canJump = Time.time + 1.0f;
                break;
        }
    }
}
