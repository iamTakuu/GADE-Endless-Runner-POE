using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    #region CUSTOMVARIABLES

    [Header("Required Components")] 
    public Animator Animator;

    
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int JumpTrigger = Animator.StringToHash("jumpTrigger");
    private static readonly int DeathTrigger = Animator.StringToHash("deathTrigger");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    #endregion

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    #region CUSTOM METHODS

    public void SetIsMoving(bool value)
    {
        Animator.SetBool(IsMoving, value);
    }

    public void SetJumpTrigger()
    {
        Animator.SetTrigger(JumpTrigger);
    }
    public void SetDeathTrigger()
    {
        Animator.SetTrigger(DeathTrigger);
    }
    #endregion

    public void SetIsJumping(bool value)
    {
        Animator.SetBool(IsJumping, value);
    }

    public void SetIsGrounded(bool value)
    {
        Animator.SetBool(IsGrounded, value);
    }

    public void SetIsDead(bool value)
    {
        Animator.SetBool(IsDead, value);
    }
}
