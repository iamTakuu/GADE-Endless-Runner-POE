

using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
 private WorldSpawner worldSpawner;
 public List<GameObject> obstaclePrefab;
 public GameObject parentSpawn;
 public List<GameObject> spawnLocations;
 

 private void Start()
 {
  worldSpawner = FindObjectOfType<WorldSpawner>();
  AddSpawnLocations(parentSpawn, spawnLocations);
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
  if (spawnLocations.Count == 0) return;

   //Randomly Picks Object From The List
   //var random = new System.Random();
   int randomObjectIndex = Random.Range(0, obstaclePrefab.Count);
   
   //Randomly Selects one of the lanes using the gameObject
   int spawnIndex = Random.Range(0, spawnLocations.Count);
   
   
   //Transform spawnPoint = parentSpawn.transform.GetChild(spawnIndex).transform;
   Transform spawnPoint = spawnLocations[spawnIndex].transform;

   //Now make a new instance
   
   
    Instantiate(obstaclePrefab[randomObjectIndex], spawnPoint.position, Quaternion.identity, transform);

 }

 private void AddSpawnLocations (GameObject PARENTSPAWN, List<GameObject> List)
 {
  if (PARENTSPAWN.transform.childCount == 0)
  {
   //spawnLocations.Add(PARENTSPAWN);
   return;
  }
  
   
  
  for (int i = 0; i < PARENTSPAWN.transform.childCount; i++)
  {
   var child = PARENTSPAWN.transform.GetChild(i).gameObject;
   spawnLocations.Add(child);
  }
  
 }
}
