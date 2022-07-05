using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Cinemachine;

public class BossBehaviour : MonoBehaviour
{
    private bool isCaughtUp = false;
    private CinemachineImpulseSource cameraImpulse;

    private Vector3 _playerPos;
    //private float catchUpSpeed = 100f;
    private bool inPosition = false;
    
    

    private float forwardSpeed = 62f;
    // Start is called before the first frame update
    

    void Start()
    {
        transform.position = new Vector3(0f,  15f, GameObject.Find("Player").transform.position.z - 30f);
        StartCoroutine(BossPattern(justSpawned: true));
        cameraImpulse = gameObject.GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.PlayerEntity.IsShielded())
        {
            GameManager.Instance.PlayerEntity.UnShield();
            return;
        }
        //If it's the player, kill the player
        if (other.CompareTag("Player"))
        {
            EventsManager.Instance.OnPlayerDeath();

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaughtUp)
        {
           
            MoveForward();
            
        }
        _playerPos = GameObject.Find("Player").transform.position;

        //set playerpos to the player's position
    }

    private IEnumerator BossPattern(bool justSpawned)
    {
        inPosition = false;
        if (justSpawned)
        {
            yield return new WaitForSeconds(8f);
        }
        StartCoroutine(CatchUpToPlayer());
        
        yield return new WaitForSeconds(5f);
        Debug.Log("Pause!");
        inPosition = true;

        StartCoroutine(Slam());
        
        yield return new WaitForSeconds(2f);
        transform.DOMoveY(15f, 0.1f, true);
        StartCoroutine(BossPattern(false));

    }

    private void EndBoss()
    {
        StartCoroutine(Leave());
    }

    private void OnEnable()
    {
        EventsManager.Instance.EndBossEvent += EndBoss;
    }

    private void OnDisable()
    {
        EventsManager.Instance.EndBossEvent -= EndBoss;
    }

    private IEnumerator Leave()
    {
        transform.DOMoveZ(GameObject.Find("Player").transform.position.z + 100f, 1f);
        //transform.position = Vector3.forward * Time.deltaTime * catchUpSpeed + transform.position;
        yield return new WaitForSeconds(0.5f);
        transform.DORotate(new Vector3(0,360,0), 1f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(0.2f);
        transform.DOMoveY( 100f, 1f);
        yield return new WaitForSeconds(0.5f);
        StopAllCoroutines();
        DOTween.KillAll();
        Destroy(gameObject);

    }
    
    private IEnumerator Slam()
    {
        isCaughtUp = false;
        inPosition = true;
        //Slam onto the ground at current position
        transform.DOMoveZ(transform.position.z + 30f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        //Move slightly up then slam down
        
        transform.DOMoveY(7f, 0.2f);
        cameraImpulse.GenerateImpulse(3f);
        
    }
    
    private void MoveForward()
    {
        transform.position = Vector3.forward * Time.deltaTime * forwardSpeed + transform.position;
    }
    
    private IEnumerator CatchUpToPlayer()
    {
        transform.DOMoveZ(GameObject.Find("Player").transform.position.z + 100f, 1f);
        //transform.position = Vector3.forward * Time.deltaTime * catchUpSpeed + transform.position;
        yield return new WaitForSeconds(1f);
        transform.DORotate(new Vector3(0,360,0), 1f, RotateMode.FastBeyond360);
        //yield return new WaitForSeconds(1f);
        isCaughtUp = true;
            
        StartCoroutine(MoveBoss());
            
    }

    private IEnumerator MoveBoss()
    {
        //int randomLane = Random.Range(0)
        while (!inPosition)
        {
            //yield return new WaitForSeconds(0.5f);

            while(_playerPos.x == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, transform.position.y, transform.position.z), 30 * Time.deltaTime);
                yield return null;
            }

           
            while (_playerPos.x > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(+18f, transform.position.y, transform.position.z), 30 * Time.deltaTime);
                yield return null;
            }

            while (_playerPos.x < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-18f, transform.position.y, transform.position.z), 30 * Time.deltaTime);
                yield return null;
            }
               
            
        }
    }
}
