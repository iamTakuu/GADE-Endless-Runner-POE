using System;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

   #region UNITY METHODS

   private void OnTriggerEnter(Collider other)
   {

      if (other.CompareTag("Magnet") || other.CompareTag("Shield"))
      {
         //then destroy that object
         Destroy(other.gameObject);
      }
      if (other.CompareTag("Player") && GameManager.Instance.PlayerEntity.IsShielded())
      {
         GameManager.Instance.PlayerEntity.UnShield();
         return;
      }

      if (other.CompareTag("Player"))
      {
         EventsManager.Instance.OnPlayerDeath();
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Magnet") || other.CompareTag("Shield"))
      {
         //then destroy that object
         Destroy(other.gameObject);
      }
   }

   private void OnBecameInvisible()
   {
      //If the object can't be seen by the camera, we can increment the score
      // GameManager.Instance.PlayerEntity.IncrementScore(); 
      EventsManager.Instance.OnPassObstacle();
   }

   #endregion
  
}
