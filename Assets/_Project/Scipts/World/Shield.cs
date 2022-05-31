using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    #region UNITY METHODS
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) 
        {
            Destroy(gameObject);
            return;
        }

        if (!other.CompareTag("Player")) return;
        GameManager.Instance.PlayerEntity.Shield();
        Destroy(gameObject);

    }

    #endregion
}
