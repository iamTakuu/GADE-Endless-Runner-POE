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

    private const float magnetisedRange = 50f;
    private const int magnetDuration = 10;
    private const int shieldDuration = 5;
    public SpinPickup pickupContainer;

    #endregion
    
    #region UNITY METHODS

    private void OnEnable()
    {
        EventsManager.Instance.PlayerDeath += GameOver;
        EventsManager.Instance.PickUpEvent += ManageSpinPickUp;
    }
    
    private void OnDisable()
    {
        EventsManager.Instance.PlayerDeath -= GameOver;
        EventsManager.Instance.PickUpEvent -= ManageSpinPickUp;
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
        //if the escape key is pressed, the game will paused using the pause menu method
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    #endregion
    
    #region CUSTOM METHODS
   
   private void ManageSpinPickUp(EventsManager.PickUpType pickUpType, bool isActive)
   {
       pickupContainer.ManageContainer(pickUpType, isActive);
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

       if (Instance.PlayerEntity.IsShielded())
       {
           StartCoroutine(ActivateShield());
       }
       else
       {
           EventsManager.Instance.OnPickUp(pickUpType: EventsManager.PickUpType.Shield, false);
           StopCoroutine(ActivateShield());
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

   public void PauseGame()
   {
       
       if (Time.timeScale == 0)
       {
           return;
       }

       Time.timeScale = 0;
       UIManager.TogglePauseScreen(true);
   }
  
   public void ResumeGame()
   {
       Time.timeScale = 1;
       UIManager.TogglePauseScreen(false);
   }

   private static void RemoveExtraPickups(string PickUpTag)
   {
       GameObject[] otherPickups = GameObject.FindGameObjectsWithTag(PickUpTag);
     
       foreach (var pickup in otherPickups)
       {
           pickup.SetActive(false);
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
       
       yield return new WaitForSeconds(magnetDuration);
       Instance.PlayerEntity.DeMagnetise();
       EventsManager.Instance.OnPickUp(pickUpType: EventsManager.PickUpType.Magnet, false);
   }
   
   private IEnumerator ActivateShield()
   {
       EventsManager.Instance.OnPickUp(pickUpType: EventsManager.PickUpType.Shield, true);
       RemoveExtraPickups("Shield");
       
       yield return new WaitForSeconds(shieldDuration);
       Instance.PlayerEntity.UnShield();
       EventsManager.Instance.OnPickUp(pickUpType: EventsManager.PickUpType.Shield, false);
   }
   
   #endregion
}
