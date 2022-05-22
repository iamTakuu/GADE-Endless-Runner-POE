using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   public PlayerEntity PlayerEntity { get; private set; }
   public WorldSpawner WorldSpawner { get; private set; }
   public UIManager UIManager { get; private set; }
   //public DirectionalLight DirectionalLight { get; private set; }

   private float magnetisedRange = 50f;
   private int magnetCoolDown = 5;
   
   private void Awake()
   {
       if (Instance !=null && Instance!=this)
       {
           Destroy(this);
           return;
       }

       Instance = this;

       PlayerEntity = GetComponentInChildren<PlayerEntity>();
       WorldSpawner = GetComponentInChildren<WorldSpawner>();
       UIManager = GetComponentInChildren<UIManager>();
       //DirectionalLight = GetComponent<DirectionalLight>();
       DontDestroyOnLoad(this);
       
   }

   

   private void Update()
   {
       
           Instance.UIManager.UpdateDistanceUI();
           Instance.UIManager.UpdateCoinUI();
           Instance.UIManager.UpdateScoreUI();
           if (!Instance.PlayerEntity.IsAlive())
           {
               Time.timeScale = 0;
               Instance.UIManager.ToggleGameOverScreen();
           }
           else
           {
               Time.timeScale = 1;
           } 
           
           StateCheck();

       
   }


   private void FixedUpdate()
   {

   }

   private void StateCheck()
   {
       if (Instance.PlayerEntity.IsMagnetised())
       {
           StartCoroutine(MagnetiseCoins());
       }
   }

   public void RestartGame()
   {
       Destroy(gameObject);
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void QuitToMenu()
   {
       Destroy(gameObject);
       Time.timeScale = 1;
       SceneManager.LoadScene(0);
   }

   private static void RemoveExtraPickups(string PickUpTag)
   {
       GameObject[] otherMagnets = GameObject.FindGameObjectsWithTag(PickUpTag);
     
       foreach (var magnet in otherMagnets)
       {
           magnet.SetActive(false);
       }
   }

   private IEnumerator MagnetiseCoins()
   {
       RemoveExtraPickups("Magnet");
       
       GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
       foreach (var coin in coins)
       {
           if (magnetisedRange >= Vector3.Distance(Instance.PlayerEntity.transform.position, coin.transform.position))
           {
               coin.transform.position = Vector3.MoveTowards(coin.transform.position,
                   Instance.PlayerEntity.transform.position, 200f * Time.deltaTime);
           }
       }
       
       
       yield return new WaitForSeconds(magnetCoolDown);
       Instance.PlayerEntity.DeMagnetise();
   }
}
