using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region VARIABLES
    
    private CharacterController PlayerController;
    private AnimationControllerScript AnimationController;
    private Vector3 move; //Used to do some vector translations
    public float forwardSpeed = 60f;
    private float distanceToGround;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    private const float laneDistance = 20f; //The distance between tow lanes Todo: Switch to representing lanes with Enums
    private bool isGrounded;
    public float horizontalSpeed = 50f;
    public float gravity = -100f;
    public float jumpHeight = 8f;
    private Vector3 velocity; //Mainly using this for gravity stuff. https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    //private bool isSliding = false;
    [SerializeField] private AudioClip[] playerAudioClips;
    
    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        PlayerController = GetComponent<CharacterController>();
        AnimationController = GetComponent<AnimationControllerScript>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.PlayerDeath += StopPlayerMovement;
    }

    private void OnDisable()
    {
        EventsManager.Instance.PlayerDeath -= StopPlayerMovement;
    }

    //There's a lot in this...sadge.
    private void Update()
    {
        move.z = forwardSpeed;

        isGrounded = GroundCheck();
        
        AnimationController.SetIsGrounded(isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
        }

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            //Allows fast falling!
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                velocity.y = -20f;
            }                

        }
        
        PlayerController.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be
        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.PlaySFX(playerAudioClips[1]);

            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlaySFX(playerAudioClips[1]);

            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future
        var targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        switch (desiredLane) 
        {
            case 0:
                targetPosition += Vector3.left  * laneDistance; 
                break;
            case 2:
                targetPosition += Vector3.right * laneDistance;
                break;
        }
        
        if (transform.position != targetPosition)
        {
            var distanceTo = targetPosition - transform.position; //Gives the raw distance between two points
            var directionVector = distanceTo.normalized * horizontalSpeed * Time.deltaTime; //Magnitude will always be 1, but direction changes.
            PlayerController.Move(directionVector.magnitude < distanceTo.magnitude ? directionVector : distanceTo);
        }
        
        PlayerController.Move(move * Time.deltaTime);

        
        AnimationController.SetIsMoving(forwardSpeed != 0);

        FallCheck();
    }

    #endregion

    #region CUSTOM METHODS

    private void StopPlayerMovement()
    {
        this.enabled = false;
    }

    /// <summary>
    /// Checks to see if the player has gone below
    /// a certain point in the Y-Axis.
    /// </summary>
    private void FallCheck()
    {
        if (transform.position.y < -10f)
        {
            EventsManager.Instance.OnPlayerDeath();
            
        }
    }
    /// <summary>
    /// This gets the total height of the collider, splits it in half
    /// then cast a beeaaaam downwards from the center to it's feet
    /// to check if grounded.
    /// </summary>
    /// <returns></returns>
    private bool GroundCheck()
    {
        //I had to make this cause the built in groundcheck sucks lmao.
        distanceToGround = GetComponent<CharacterController>().bounds.size.y / 2;
        return (Physics.Raycast(transform.position, Vector3.down, distanceToGround-2f));
    }
    
    /// <summary>
    /// Used to jump. Calculates distance to go in Y-axis
    /// by using the jumpHeight var and the Gravity value.
    /// </summary>
    private void Jump()
    { 
        //StopCoroutine(Slide());
        AnimationController.SetJumpTrigger();
        AudioManager.Instance.PlaySFX(playerAudioClips[0]);

        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }
    
    #endregion
    


   
    
    
    
    
    

    
    
}
