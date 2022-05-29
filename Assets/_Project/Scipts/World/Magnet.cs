using UnityEngine;

public class Magnet : MonoBehaviour
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
      GameManager.Instance.PlayerEntity.Magnetise();
      Destroy(gameObject);

   }
   
   
   #endregion
}
