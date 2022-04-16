using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = System.Random;

public class WorldSpawner : MonoBehaviour
{
    public List<GameObject> worldTiles;
    private Vector3 tileSpawnPoint;

    public void SpawnWorld()
    {
        
        
        
        
        //This will create an instance of WorldTile prefab at the End of the last tile
        //Quaternion.identiy == no rotation.
        GameObject tempTile = Instantiate(ReturnRandomObj(), tileSpawnPoint, Quaternion.identity);
        //This grabs the second child of WorldTile Prefab. Then adds its position to tileSpawnPoint
        tileSpawnPoint = tempTile.transform.GetChild(1).transform.position; 
    }

    private GameObject ReturnRandomObj()
    {
        
        return worldTiles[Random.Range(0, worldTiles.Count)];

    }
    
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
           SpawnWorld(); 
        }

    }
}
