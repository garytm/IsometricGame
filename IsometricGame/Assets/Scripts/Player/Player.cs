using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*Creating a reference to the animator attached to the player character which 
     * allows the animations to be controlled from this class*/
    Animator animator;
    /*An float to control the height the player can jump to*/
    public float jumpSpeed;
    /*A float used to influence the players speed*/
    public float moveSpeed;
    /*A float to time animations*/
    float animationTimer = 2.0f;
    /*A float to check if the player can jump*/
    float canJump = 0.0f;
    /*A boolean to check if the player is currently jumping*/
    bool isJumping;
    /*A  vector used for the players direction*/
    Vector3 direction;

    void Start()
    {
    }
    void Update()
    {
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
        /*Ensuring movement works per-frame*/
        Movement();
    }

    void Movement()
    {
        animator.SetInteger("isJumping", 0);
        animator.SetBool("isWalking", false);
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalking", true);
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
         if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalking", true);
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
         if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalking", true);
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        }
         if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Time.time > canJump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetInteger("isJumping", 1);
                transform.Translate(Vector2.up * 25 * Time.deltaTime, 0);
                canJump = Time.time + 2.0f;
            }
        }
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
