using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-110)]
public class EventsManager : MonoBehaviour
{
    #region GLOBAL INSTANCE

    public static EventsManager Instance;
    
    
    
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


    #endregion
}
