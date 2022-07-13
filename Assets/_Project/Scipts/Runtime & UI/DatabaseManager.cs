using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

[DefaultExecutionOrder(-80)]
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;
    public TMP_InputField userNameInputField;
    public Leaderboard Leaderboard { get; private set; }

    private void Start()
    {
        StartCoroutine(DatabaseSetupRoutine());
    }
    private void Awake()
    {
        if (Instance !=null && Instance!=this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        Leaderboard = GetComponentInChildren<Leaderboard>();

        DontDestroyOnLoad(this);
    }
    
    IEnumerator DatabaseSetupRoutine()
    {
        yield return LoginRoutine();
        yield return Instance.Leaderboard.GrabHighScores();
    }

    public void SetUserName()
    {
        LootLockerSDKManager.SetPlayerName(userNameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Usrname Set");
            }
            else
            {
                Debug.Log("Error "+response.Error);
            }
        });
    }

    IEnumerator LoginRoutine()
    {
        bool authenticated = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Log in sucess");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                authenticated = true;
            }
            else
            {
                Debug.Log("Session failed");
                authenticated = true;
            }
        });
        yield return new WaitWhile((() => authenticated == false));
    }

    
}
