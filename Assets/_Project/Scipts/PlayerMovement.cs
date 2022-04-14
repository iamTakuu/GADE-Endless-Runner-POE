using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController;
    //public float moveSpeed = 5f;
    //private Rigidbody rigidBody;
    private float horizontalInput;
    public float forwardSpeed = 2f;
    public float horizontalSpeed = 2f;
    private Vector3 targetPos;
    private Vector3 movement;

    public float jumpForce = 3f;
    private bool isGrounded;
    private bool _jump;

    public float distanceToGround;
    //[Header("Required Component")]
    public AnimationControllerScript AnimationController;

    public int targetLane = 1; //0: left, 1 Mid, 2 right
    public float laneDist = 3f; //Dist between lanes


    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        //rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //To do: Make an input Checking method.
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetLane++;
            if (targetLane==3)
            {
                targetLane = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetLane--;
            if (targetLane==-1)
            {
                targetLane = 0;
            }
        }

        targetPos = transform.position.z * Vector3.forward;
        if (targetLane == 0) //Left
        {
            targetPos = Vector3.left * laneDist;
        }else if (targetLane == 2)
        {
            targetPos = Vector3.right * laneDist;
        }

        //transform.position = targetPos;
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }
        
        
        

        movement = Vector3.zero;
        //https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        //.Move is called with the exact x, y and z coordinates, but since we can only move LEFT RIGHT AND FORWARD
        //I assigned the values manually.
        
        movement.x = (targetPos - transform.position).normalized.x * horizontalSpeed; //Uses the targetPos calculated earlier then just multiplies by the movement speed.
        movement.y = -0.1f; //Can't be set to zero...this breaks the animator and groundcheck...
        movement.z = forwardSpeed; //set to forwardspeed because player only goes forward.
        

        

        
        if (_jump)
        {
            //rigidBody.velocity += (Vector3.up * jumpForce);
            
            
        }
        
        GroundCheck();
        AnimationController.SetIsMoving(forwardSpeed != 0);
        AnimationController.SetIsJumping(_jump);
        _jump = false;
    }

    private void FixedUpdate()
    {
        //Now we can move.
        playerController.Move(movement * Time.deltaTime);
    }

    private void GroundCheck()
    {
        //This gets the total height of the collider, splits it in half
        //then cast a beeaaaam downwards from the center to it's feet
        distanceToGround = GetComponent<CharacterController>().bounds.size.y / 2;
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, distanceToGround));
        if (isGrounded)
        {
            Debug.Log("Grounded");
        }
        
    }
}
