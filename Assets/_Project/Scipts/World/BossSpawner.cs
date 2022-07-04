using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    private Vector3 spawnPoint;

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
