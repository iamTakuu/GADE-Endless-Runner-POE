using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private GameObject[] _menusPanels;

    private void Awake()
    {
        StartButton.onClick.AddListener(EventsManager.Instance.StartGame);
    }

    public void OpenOptions()
    {
        _menusPanels[0].SetActive(false);
        _menusPanels[1].SetActive(true);
        _menusPanels[2].SetActive(false);

    }
    public void ReturnToMenu()
    {
        _menusPanels[0].SetActive(true);
        _menusPanels[1].SetActive(false);
        _menusPanels[2].SetActive(false);

    }

    public void OpenLeaderBoard()
    {
        _menusPanels[0].SetActive(false);
        _menusPanels[1].SetActive(false);
        _menusPanels[2].SetActive(true);
        DatabaseManager.Instance.Leaderboard.UpdateLeaderBoard();
    }
}
