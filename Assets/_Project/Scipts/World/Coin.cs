using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 50f;

    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Obstacle") || other.CompareTag("Magnet")) //If the coin collides with an obstacle
        { 
           if (GameManager.Instance.PlayerEntity.IsMagnetised()) return; 
           Destroy(gameObject);
           return;
           
        }
       
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.PlayerEntity.CollectCoin();
        
        
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    
}
