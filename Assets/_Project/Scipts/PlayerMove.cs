using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private AnimationControllerScript AnimationController;
    private Vector3 move;
    public float forwardSpeed = 30f;
    

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 10f;//The distance between tow lanes

    public bool isGrounded;
    public float horizontalSpeed = 40f;

    

    public float gravity = -30f;
    public float jumpHeight = 10f;
    private Vector3 velocity;

    
    private bool isSliding = false;

    

    //bool toggle = false;

    void Awake()
    {
        
        controller = GetComponent<CharacterController>();
        AnimationController = GetComponent<AnimationControllerScript>();

    }

    void Update()
    {
        
        //animator.SetBool("isGameStarted", true);
        move.z = forwardSpeed;

        isGrounded = controller.isGrounded;
        AnimationController.SetIsGrounded(isGrounded);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            //Allows fast falling!
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
            {
                velocity.y = -15f;
            }                

        }
        controller.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future
        var targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        switch (desiredLane)
        {
            case 0:
                targetPosition += Vector3.left * laneDistance;
                break;
            case 2:
                targetPosition += Vector3.right * laneDistance;
                break;
        }

        //transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            var distanceTo = targetPosition - transform.position; //Gives the raw distance between two points
            var directionVector = distanceTo.normalized * horizontalSpeed * Time.deltaTime; //Magnitude will always be 1, but direction changes.
            controller.Move(directionVector.magnitude < distanceTo.magnitude ? directionVector : distanceTo);
        }
        
        controller.Move(move * Time.deltaTime);
        
        AnimationController.SetIsMoving(forwardSpeed != 0);
    }

    private void Jump()
    {   
        //StopCoroutine(Slide());
       // animator.SetBool("isSliding", false);
        //animator.SetTrigger("jump");
        //controller.center = Vector3.zero;
        //controller.height = 2;
        //isSliding = false;
        AnimationController.SetJumpTrigger();
   
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }
    
    

}
