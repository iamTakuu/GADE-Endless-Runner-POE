using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    #region UNITY METHODS
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Magnet")) //If the coin collides with an obstacle
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
