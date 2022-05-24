using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldSpawner : MonoBehaviour
{
    #region VARIABLES

    public List<GameObject> worldTiles;
    private Vector3 tileSpawnPoint;
    private readonly float zAxisDist = 50f;

    #endregion
    
    #region CUSTOM METHODS

    public void SpawnWorld()
    {
        //This will create an instance of WorldTile prefab at the End of the last tile
        //Quaternion.identity == no rotation.
        
        Instantiate(ReturnRandomTile(), tileSpawnPoint, Quaternion.identity);
        tileSpawnPoint.z += zAxisDist;
    }
    
    private GameObject ReturnRandomTile()
    {
        if (GameManager.Instance.PlayerEntity.transform.position.z < 10f)
        {
            return worldTiles[0]; //Makes sure the first tile is always solid ground.
        }
        return worldTiles[Random.Range(0, worldTiles.Count)];

    }

    #endregion

    #region UNITY METHODS
    
    private void Start()
    {
        for (var i = 0; i < 10; i++)
        {
            SpawnWorld(); 
        }
    }
    
    #endregion
}
