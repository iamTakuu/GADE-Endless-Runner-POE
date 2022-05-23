using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-110)]
public class EventsManager : MonoBehaviour
{
    #region GLOBAL INSTANCE

    public static EventsManager Instance;

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

    #endregion
}
