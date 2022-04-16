using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
   
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("Ouch");
      if (!other.CompareTag("Player")) return;
      other.GetComponent<PlayerMove>().Die();
       
      
   }
}
