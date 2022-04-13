

using UnityEngine;

public class WorldTile : MonoBehaviour
{
 private WorldSpawner worldSpawner;
 
 private void Start()
 {
  worldSpawner = FindObjectOfType<WorldSpawner>();
  
 }


 private void OnTriggerExit(Collider other)
 {
  if (other.CompareTag("Player"))
  {
   //When the player exits any Box Colider in the world,
   //spawn the next one.
   worldSpawner.SpawnWorld();
   Destroy(gameObject, 1); //Destroys the previous WorldTile after 1 second of exiting.
  }
  
 }
}
