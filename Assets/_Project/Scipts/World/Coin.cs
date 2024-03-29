using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region VARIABLES
    
    public float rotationSpeed = 50f;
    [SerializeField] private AudioClip coinSFX;
    
    #endregion

    #region UNITY METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && (!GameManager.Instance.PlayerEntity.IsMagnetised()))
        {
            Destroy(gameObject);
        }
       
        if (other.CompareTag("Obstacle") || other.CompareTag("Magnet") || other.CompareTag("Shield")) //If the coin collides with an obstacle
        { 
           if (GameManager.Instance.PlayerEntity.IsMagnetised()) return; 
           Destroy(gameObject);
           return;
           
        }
       
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.PlayerEntity.CollectCoin();
        AudioManager.Instance.PlaySFX(coinSFX);
        
        
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
    }
    
    #endregion
    
}
