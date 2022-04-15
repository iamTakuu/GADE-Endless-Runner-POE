using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
   public PlayerMove playerMove;


   private void Start()
   {
      playerMove = FindObjectOfType<PlayerMove>();
   }

   
   private void OnCollisionEnter(Collision collision)
   {
      Debug.Log("Ouch");
      if (collision.gameObject.name == "Player")
      {
         //Kill the player lol.
         playerMove.Die();
      }
   }

   
}
