using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rigidBody;
    private float horizontalInput;
    public float horizontalMultiply = 2f;

    public float jumpForce = 3f;
    private bool isGrounded;
    private bool _jump;

    public float distanceToGround;
    //[Header("Required Component")]
    public AnimationControllerScript AnimationController;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //To do: Make an input Checking method.
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }
    }

    private void FixedUpdate()
    {

       
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalInput * moveSpeed * Time.fixedDeltaTime * horizontalMultiply;
        

        
        if (_jump)
        {
            rigidBody.velocity += (Vector3.up * jumpForce);
            
            
        }
        rigidBody.MovePosition(rigidBody.position + forwardMovement + horizontalMovement);
        GroundCheck();
        AnimationController.SetIsMoving(forwardMovement != Vector3.zero);
        AnimationController.SetIsJumping(_jump);
        _jump = false;
        
    }

    

    private void GroundCheck()
    {
        //This gets the total height of the collider, splits it in half
        //then cast a beeaaaam downwards from the center to it's feet
        distanceToGround = GetComponent<Collider>().bounds.size.y / 2;
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, distanceToGround));
        
    }
}
