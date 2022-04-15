using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A more optimal version of the Original movement script.
/// </summary>
public class PlayerMovement_V2 : MonoBehaviour
{
    private CharacterController PlayerController;
    private AnimationControllerScript AnimationController;
    private Vector3 movement;
    private Vector3 targetPosition;
    private float horizontalInput;
    public float distanceToGround;
    private bool isGrounded;
    
    public float gravityValue = -9.81f;
    public float jumpHeight = 6f;
    public float forwardSpeed = 30f;
    private Vector3 playerVelocity;
    public float horizontalSpeed = 40f;
    
    //public float maxSpeed;

    private int targetLane = 1;//0:left, 1:middle, 2:right ToDo: change to enum
    public float laneDistance = 10f;//The distance between tow lanes
    
    void Awake()
    {
        
        PlayerController = GetComponent<CharacterController>();
        AnimationController = GetComponent<AnimationControllerScript>();

    }

    void Update()
    {
       movement.z = forwardSpeed; //Makes sure we're always moving forward.
       
       //PlayerController.Move(movement * Time.deltaTime);
       if (!GroundCheck())
       {
           Debug.Log("Floating");
       }
       else
       {
           Debug.Log("Grounded");
       }



       isGrounded = PlayerController.isGrounded;
       // Changes the height position of the player..
       if (Input.GetButtonDown("Jump") && isGrounded)
       {
           playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
       }
       
       
       
       
       ProcessLanesInput();
       GroundCheck();

        

        
        AnimationController.SetIsMoving(forwardSpeed != 0);
    }

   
    private void ProcessLanesInput()
    {
        //Gather the inputs on which lane the player intends to go to
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) )
        {
            targetLane++;
            if (targetLane == 3)
                targetLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetLane--;
            if (targetLane == -1)
                targetLane = 0;
        }

        //Calculate where we should be in the future
        targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        switch (targetLane)
        {
            case 0:
                targetPosition += Vector3.left * laneDistance;
                break;
            case 2:
                targetPosition += Vector3.right * laneDistance;
                break;
        }
    }
    
    private bool GroundCheck()
    {
        //This gets the total height of the collider, splits it in half
        //then cast a beeaaaam downwards from the center to it's feet
        distanceToGround = GetComponent<CharacterController>().bounds.size.y / 2;
        return (Physics.Raycast(transform.position, Vector3.down, distanceToGround-2f));
        
        
    }

    private void FixedUpdate() //Running all the actual movement so that physics isn't tied to framerate lol.
    {
        
        

        //Checking to see if the player is not currently at the 
        //target position.
        if (transform.position != targetPosition)
        {
            var distanceTo = targetPosition - transform.position; //Gives the raw distance between two points
            var directionVector = distanceTo.normalized * horizontalSpeed * Time.deltaTime; //Magnitude will always be 1, but direction changes.
            
            // if (directionVector.magnitude < distanceTo.magnitude)//The only time this is false, is when you're already at the targetPos
            // {
            //     playerController.Move(directionVector); //We successfully move to the new lane. With the intended speed.
            // }
            // else
            // {
            //     playerController.Move(distanceTo);//We "Move" appropriatley....simply keeps it where it currently is.
            // }
            
            PlayerController.Move(directionVector.magnitude < distanceTo.magnitude ? directionVector : distanceTo); //Shorthand version of above code.
        }

        

        PlayerController.Move(movement * Time.deltaTime);
        playerVelocity.y += gravityValue * Time.deltaTime;
        PlayerController.Move(playerVelocity * Time.deltaTime);
        

    }
}
