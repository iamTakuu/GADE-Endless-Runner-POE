using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   public PlayerEntity PlayerEntity { get; private set; }
   public WorldSpawner WorldSpawner { get; private set; }
   public UIManager UIManager { get; private set; }


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
   }

   private void Update()
   {
       Instance.UIManager.UpdateDistanceUI();
       Instance.UIManager.UpdateCoinUI();
       Instance.UIManager.UpdateScoreUI();
   }
   //To Do: Add the PlayerEntity stuff here so I can easily access it and display on the UI and all that Jazz.
}
