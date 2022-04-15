

using UnityEngine;

public class WorldTile : MonoBehaviour
{
 private WorldSpawner worldSpawner;
 public GameObject obstaclePrefab;
 
 private void Start()
 {
  worldSpawner = FindObjectOfType<WorldSpawner>();
  SpawnObstacle();
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

 void SpawnObstacle()
 {
   //Randomly Selects one of the lanes using the gameObjects
   int spawnIndex = Random.Range(2, 5);
   Transform spawnPoint = transform.GetChild(spawnIndex).transform;
   
   //Now make a new instance
   Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);

 }

 
}
