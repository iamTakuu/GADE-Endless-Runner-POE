using System;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    #region ANIMATION VARIABLES

    [Header("Required Components")] 
    public Animator Animator;
    
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int JumpTrigger = Animator.StringToHash("jumpTrigger");
    private static readonly int DeathTrigger = Animator.StringToHash("deathTrigger");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.PlayerDeath += SetDeathTrigger;
    }

    private void OnDisable()
    {
        EventsManager.Instance.PlayerDeath -= SetDeathTrigger;
    }

    #endregion

    #region ANIMATION METHODS

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

    #endregion
   
}
