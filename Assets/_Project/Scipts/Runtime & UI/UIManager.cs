using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region VARIABLES
    
    [Header("Text Mesh Elements")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finaldistanceText;
    public TextMeshProUGUI finalcoinText;
    public TextMeshProUGUI obstaclesPassed;
    public TextMeshProUGUI finalscoreText;
    public GameObject GameOverScreen;
    public GameObject PauseMenuScreen;
   
    #endregion

    #region UIMETHODS

    public void UpdateDistanceUI()
    {
        distanceText.text = ("Distance: " + GameManager.Instance.PlayerEntity.playerDistance +"m");
    }

    public void UpdateCoinUI()
    {
        coinText.text = ("Coins: " + GameManager.Instance.PlayerEntity.playerCoinCount);
    }

    public void UpdateScoreUI()
    {
        scoreText.text = ("Obstacles: " + GameManager.Instance.PlayerEntity.obstacleScore);
    }

    public void ToggleGameOverScreen()
    {
        obstaclesPassed.text = "Obstacles Passed: "+GameManager.Instance.PlayerEntity.obstacleScore;
        finaldistanceText.text= "Distance Travelled: "+GameManager.Instance.PlayerEntity.playerDistance +"m";
        finalcoinText.text = "Total Coins: "+GameManager.Instance.PlayerEntity.playerCoinCount;
        finalscoreText.text = "Final Score: "+GameManager.Instance.PlayerEntity.playerScore;
        GameOverScreen.SetActive(true);
    }

    #endregion

    public void TogglePauseScreen(bool toggle)
    {
        PauseMenuScreen.SetActive(toggle);
    }
    
}
