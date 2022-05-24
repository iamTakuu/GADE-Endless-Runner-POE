using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    #region GAMEMANAGER SINGLETON

    public static GameManager Instance { get; private set; }


    #endregion

    #region SINGLETON VARIABLES

    public PlayerEntity PlayerEntity { get; private set; }
    public WorldSpawner WorldSpawner { get; private set; }
    public UIManager UIManager { get; private set; }
    //public DirectionalLight DirectionalLight { get; private set; }

    #endregion

    #region VARIABLES

    private float magnetisedRange = 50f;
    private int magnetCoolDown = 5;
    public GameObject pickupContainer;

    #endregion
    
    #region UNITY METHODS

    private void OnEnable()
    {
        EventsManager.Instance.PlayerDeath += GameOver;
        EventsManager.Instance.PickUpEvent += SpinPickUp;
    }
    
    private void OnDisable()
    {
        EventsManager.Instance.PlayerDeath -= GameOver;
        EventsManager.Instance.PickUpEvent -= SpinPickUp;
    }
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
        DontDestroyOnLoad(this);
    }
   
    private void Update()
    {
       
        Instance.UIManager.UpdateDistanceUI();
        Instance.UIManager.UpdateCoinUI();
        Instance.UIManager.UpdateScoreUI();
           
           
        StateCheck();
    }

    #endregion
    
    #region CUSTOM METHODS
   
   private void SpinPickUp(EventsManager.PickUpType pickUpType, bool isActive)
   {
       if (isActive)
       {
           pickupContainer.SetActive(true);
       }
       else
       {
           pickupContainer.SetActive(false);
       }
   }
   
   private void GameOver()
   {
       StartCoroutine(ShowGameOverUI(1.5f));
   }
   
   private IEnumerator ShowGameOverUI(float waitDuration)
   {
       
       yield return new WaitForSeconds(waitDuration);
       Time.timeScale = 0;
       Instance.UIManager.ToggleGameOverScreen();
       
       
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
       Time.timeScale = 1;
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
       EventsManager.Instance.OnPickUp(pickUpType: EventsManager.PickUpType.Magnet, true);
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
       EventsManager.Instance.OnPickUp(pickUpType: EventsManager.PickUpType.Magnet, false);
   }
   
   #endregion
}
