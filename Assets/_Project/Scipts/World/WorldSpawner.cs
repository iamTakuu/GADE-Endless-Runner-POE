using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldSpawner : MonoBehaviour
{
    #region VARIABLES
    [Header("Level 1 Tiles")]
    [SerializeField] private List<GameObject> lv1_Tiles;
    
    [Header("Level 2 Tiles")]
    [SerializeField] private List<GameObject> lv2_Tiles;
    
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
        if (GameManager.Instance.CurrentLevel == GameManager.GameLevel.LEVELONE)
        {
            if (GameManager.Instance.PlayerEntity.transform.position.z < 10f || GameManager.Instance.bossPresent)
            {
                return lv1_Tiles[0]; //Makes sure the first tile is always solid ground.
            }
            return lv1_Tiles[Random.Range(0, lv1_Tiles.Count)];
        }

        if (GameManager.Instance.PlayerEntity.transform.position.z < 10f || GameManager.Instance.bossPresent)
        {
            return lv2_Tiles[0]; //Makes sure the first tile is always solid ground.
        }
        return lv2_Tiles[Random.Range(0, lv2_Tiles.Count)];




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
