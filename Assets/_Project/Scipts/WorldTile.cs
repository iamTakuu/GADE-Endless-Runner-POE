

using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
 private WorldSpawner worldSpawner;
 public List<GameObject> obstaclePrefab;
 public GameObject coinPrefab;
 public GameObject parentSpawn;
 public List<GameObject> spawnLocations;
 

 private void Start()
 {
  worldSpawner = FindObjectOfType<WorldSpawner>();
  AddSpawnLocations(parentSpawn, spawnLocations);
  SpawnObstacle();
  SpawnCoins();
  
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

   //We randomise an Index for the obstacle we want.
   int randomObjectIndex = Random.Range(0, obstaclePrefab.Count);
   
   //Randomly Selects one of the lanes using the gameObject
   int spawnIndex = Random.Range(0, spawnLocations.Count);
   
   
   //Based on the lane selected, assign the Physical spawn point of the obstacle.
   Transform spawnPoint = spawnLocations[spawnIndex].transform;

   //Now make a new instance with the random obstacle at the desired location.
   Instantiate(obstaclePrefab[randomObjectIndex], spawnPoint.position, Quaternion.identity, transform);

 }

 private void AddSpawnLocations (GameObject PARENTSPAWN, List<GameObject> List)
 {
  if (PARENTSPAWN.transform.childCount == 0)
  {
   //spawnLocations.Add(PARENTSPAWN); todo: this lets obstacles be spawned on single lanes, but it's off for now. FIX
   return;
  }
  
   
  
  for (int i = 0; i < PARENTSPAWN.transform.childCount; i++)
  {
   var child = PARENTSPAWN.transform.GetChild(i).gameObject;
   spawnLocations.Add(child);
  }
  
 }

 private void SpawnCoins()
 {
  int coinCount = 4;
  for (int i = 0; i < coinCount; i++)
  {
   GameObject tempCoin = Instantiate(coinPrefab, transform);//We grab transform to make it a child of the tile, so it gets deleted OnCollisonExit.
   tempCoin.transform.position = RandomisePoint(transform.GetChild(0).GetComponent<Collider>());
  }
 }

 Vector3 RandomisePoint(Collider collider)
 {
  //Vector3 point = new Vector3(0, 0, transform.position.z);
  Vector3 point;
  if (collider.bounds.extents.x == collider.bounds.extents.z) // If it's a normal sized plane, spawn them all randomly.
  {
   point = new Vector3(
    Random.Range(collider.bounds.min.x, collider.bounds.max.x)+2f, //Padding to the X axis
    Random.Range(collider.bounds.min.y, collider.bounds.max.y),
    Random.Range(collider.bounds.min.z, collider.bounds.max.z));
  }
  else //If it's the single one, spawn them right in the centre
  {
   point = new Vector3(0, 0, transform.position.z);

  }

  
  point.z += 5; //Padding
  point.y = 2; //Keeps coins on the floor
  return point;
 }
 
}
