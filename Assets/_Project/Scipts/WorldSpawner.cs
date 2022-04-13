using System;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public GameObject worldTile;
    private Vector3 tileSpawnPoint;

    public void SpawnWorld()
    {
        //This will create an instance of WorldTile prefab at the End of the last tile
        //Quaternion.identiy == no rotation.
        GameObject tempTile = Instantiate(worldTile, tileSpawnPoint, Quaternion.identity);
        //This grabs the second child of WorldTile Prefab. Then adds its position to tileSpawnPoint
        tileSpawnPoint = tempTile.transform.GetChild(1).transform.position; 
    }
    
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
           SpawnWorld(); 
        }

    }
}
