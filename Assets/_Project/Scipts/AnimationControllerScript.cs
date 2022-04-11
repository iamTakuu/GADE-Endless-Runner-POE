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
 
    #endregion
}
