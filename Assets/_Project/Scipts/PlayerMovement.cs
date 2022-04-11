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

    //[Header("Required Component")]
    public AnimationControllerScript AnimationController;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {

       
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalInput * moveSpeed * Time.fixedDeltaTime * horizontalMultiply;
        rigidBody.MovePosition(rigidBody.position + forwardMovement + horizontalMovement);

        AnimationController.SetIsMoving(forwardMovement != Vector3.zero);


    }
}
