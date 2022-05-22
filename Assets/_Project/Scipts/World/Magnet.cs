using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Magnet : MonoBehaviour
{
   
   //Need to figure out how to make this pickupable
   #region VARIABLES

   
   
   #endregion
   
   #region UNITY METHODS

   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Obstacle")) //If the coin collides with an obstacle
      {
         Destroy(gameObject);
         return;
      }

      if (other.CompareTag("Player"))
      {
         GameManager.Instance.PlayerEntity.Magnetise();
         Destroy(gameObject);
         
      }
      
   }

   #endregion

   #region CUSTOMMETHODS

   

   #endregion
 
}