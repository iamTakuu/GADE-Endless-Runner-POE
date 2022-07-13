using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-90)]
public class EventsManager : MonoBehaviour
{
    #region GLOBAL INSTANCE

    public static EventsManager Instance { get; private set; }
    
    public enum PickUpType
    {
        Magnet,
        Shield
    }
    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (Instance !=null && Instance!=this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    #region CHARACTER EVENTS

    public event Action PlayerDeath;
    public void OnPlayerDeath() => PlayerDeath?.Invoke();

    public event Action<PickUpType, bool> PickUpEvent;
    public void OnPickUp(PickUpType pickUpType, bool isActive) => PickUpEvent?.Invoke(pickUpType,isActive);

    public event Action BossEvent;
    public void OnBossEvent() => BossEvent?.Invoke();
    
    public event Action EndBossEvent;
    public void OnEndBossEvent() => EndBossEvent?.Invoke();

    public event Action<string> BMGSwitch;
    public void OnBMGSwitch(string scene) => BMGSwitch?.Invoke(scene);

    public event Action PassObstacle;
    public void OnPassObstacle() => PassObstacle?.Invoke();
    #endregion
    
    public void StartGame()
    {
        Instance.OnBMGSwitch("Level1");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        
    }
}
