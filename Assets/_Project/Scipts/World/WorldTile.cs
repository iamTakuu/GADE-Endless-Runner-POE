

using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
 #region VARIABLES
 
 public List<GameObject> obstaclePrefab;
 public GameObject coinPrefab;
 public GameObject magnetPrefab;
 public GameObject obstacleParentSpawn;
 public GameObject pickupParentSpawn;
 public List<GameObject> obstacleSpawnLocations;
 public List<GameObject> pickupSpawnLocations;
 
 #endregion



 #region UNITY METHODS

 private void Start()
 {
  int magnetProbability = Random.Range(0, 3);
  
  
  AddObstacleSpawnLocations(obstacleParentSpawn);
  AddCoinSpawnLocations(pickupParentSpawn);
  SpawnObstacle();
  if (magnetProbability == 1)
  {
   SpawnMagnets();

  }
  SpawnLaneCoins();
 }

 private void OnTriggerExit(Collider other)
 {
  if (other.CompareTag("Player"))
  {
   //When the player exits any Box Colider in the world,
   //spawn the next one.
   GameManager.Instance.WorldSpawner.SpawnWorld();
   Destroy(gameObject, 1); //Destroys the previous WorldTile after 1 second of exiting.
  }
  
 }

 #endregion

 #region CUSTOMMETHODS

 private void SpawnObstacle()
 {
  if (obstacleSpawnLocations.Count == 0) return;
  
   //We randomise an Index for the obstacle we want.
   int randomObjectIndex = Random.Range(0, obstaclePrefab.Count);
   
   //Randomly Selects one of the lanes using the gameObject
   int spawnIndex = Random.Range(0, obstacleSpawnLocations.Count);
   
   
   //Based on the lane selected, assign the Physical spawn point of the obstacle.
   Transform spawnPoint = obstacleSpawnLocations[spawnIndex].transform;

   //Now make a new instance with the random obstacle at the desired location.
   Instantiate(obstaclePrefab[randomObjectIndex], spawnPoint.position, Quaternion.identity, transform);
 }

 private void SpawnLaneCoins()
 {
  const int coinsToSpawn = 5;
  float zPadding = 0;

  if (pickupSpawnLocations == null) return;
  int spawnIndex = Random.Range(0, pickupSpawnLocations.Count);
  Transform spawnPoint = pickupSpawnLocations[spawnIndex].transform;
  
  
  for (int i = 0; i < coinsToSpawn-1; i++)
  {
   GameObject tempCoin = Instantiate(coinPrefab, transform);//We grab transform to make it a child of the tile, so it gets deleted OnCollisonExit.
   tempCoin.transform.position = spawnPoint.position + Vector3.forward * zPadding;
   zPadding += 10f;
  }

 }

 private void SpawnMagnets()
 {

  if (GameManager.Instance.PlayerEntity.IsMagnetised()) return;
  
  if (pickupSpawnLocations == null) return;
  
  float zPos = Random.Range(0, 6) * 10;
  int spawnIndex = Random.Range(0, pickupSpawnLocations.Count);
  
  Transform spawnPoint = pickupSpawnLocations[spawnIndex].transform;
  GameObject tempMag = Instantiate(magnetPrefab, transform);
  tempMag.transform.position = spawnPoint.position + Vector3.forward * zPos;

 }

 private void AddObstacleSpawnLocations (GameObject obstacleParentSpawn)
 {
  if (obstacleParentSpawn.transform.childCount == 0)
  {
   //spawnLocations.Add(PARENTSPAWN); todo: this lets obstacles be spawned on single lanes, but it's off for now. FIX
   return;
  }
  
  for (int i = 0; i < obstacleParentSpawn.transform.childCount; i++)
  {
   var child = obstacleParentSpawn.transform.GetChild(i).gameObject;
   obstacleSpawnLocations.Add(child);
  }
  
 }
 private void AddCoinSpawnLocations (GameObject coinParentSpawn)
 {
  if (coinParentSpawn.transform.childCount == 0)
  {
   pickupSpawnLocations = null;
   return;
  }
  
  for (int i = 0; i < coinParentSpawn.transform.childCount; i++)
  {
   var child = coinParentSpawn.transform.GetChild(i).gameObject;
   pickupSpawnLocations.Add(child);
  }
  
 }
 

 #endregion

 
 
}
