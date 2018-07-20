﻿using System.Collections;
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

    public float canJump = 0.0f;
    /*The rigid body that will be used to manipulate the player*/
    Rigidbody rigid;
    Vector3 direction;
    FollowCamera followCamera;
    /*An enumerator to hold the states available to the player*/
    public enum State { Idle, Walking, Running, Jumping }
    public State state;

    void Start()
    {
        /*The initial dog state should be idle*/
        state = State.Idle;
        /*A reference to the rigidbody component*/
        rigid = GetComponent<Rigidbody>();
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
        followCamera = FindObjectOfType<FollowCamera>();
    }
    void Update()
    {
        /*Ensuring the movement is updated every frame*/
        Movement(direction);
        /*Ensuring the states are updated every frame*/
        UpdateStates();
    }
    public void Movement(Vector3 direction)
    {
        state = State.Idle;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A))
        || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))
        {
            state = State.Walking;
            /*Slerps the players rotation based on the look rotation and their current movement*/
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.10f);
            transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = State.Running;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.10f);
                transform.Translate(direction * walkSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > canJump)
        {
            state = State.Jumping;
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
