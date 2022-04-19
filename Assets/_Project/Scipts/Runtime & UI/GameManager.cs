using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   public PlayerEntity PlayerEntity { get; private set; }
   public WorldSpawner WorldSpawner { get; private set; }
   public UIManager UIManager { get; private set; }
   //public DirectionalLight DirectionalLight { get; private set; }


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

   
}
