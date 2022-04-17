using System;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
   
   private void OnTriggerEnter(Collider other)
   {
      //Debug.Log("Ouch");
      if (!other.CompareTag("Player")) return;
      GameManager.Instance.PlayerEntity.Die();
      
   }

   private void OnBecameInvisible()
   {
      //If the object can't be seen by the camera, we can increment the score
      GameManager.Instance.PlayerEntity.IncrementScore(); 
   }
}
