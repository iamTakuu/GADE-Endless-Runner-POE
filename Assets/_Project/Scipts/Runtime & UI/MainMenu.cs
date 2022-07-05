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
}
