using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    private Vector3 spawnPoint;

    private void Awake()
    {
        //Set the spawnpoint to be 50 units ahead of the player's current position
        //spawnPoint = new Vector3(GameManager.Instance.PlayerEntity.transform.position.x, GameManager.Instance.PlayerEntity.transform.position.y,
            //GameManager.Instance.PlayerEntity.transform.position.z );
    }

    private void OnEnable()
    {
        EventsManager.Instance.BossEvent += SpawnBoss;
    }

    private void OnDisable()
    {
        EventsManager.Instance.BossEvent -= SpawnBoss;
    }
    

    private void SpawnBoss()
    {
        Instantiate(boss, new Vector3(0,0,0), Quaternion.identity);
    }
    
}
