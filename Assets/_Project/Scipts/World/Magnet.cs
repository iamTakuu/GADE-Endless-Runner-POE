using UnityEngine;

public class Magnet : MonoBehaviour
{
   #region UNITY METHODS
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Obstacle")) //If the coin collides with an obstacle
      {
         Destroy(gameObject);
         return;
      }
      if (!other.CompareTag("Player")) return;
      GameManager.Instance.PlayerEntity.Magnetise();
      Destroy(gameObject);

   }
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Obstacle") || other.CompareTag("Shield"))
      {
         Destroy(gameObject);
      }
   }
   
   #endregion
}
