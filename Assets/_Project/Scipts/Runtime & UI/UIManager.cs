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
    public TextMeshProUGUI finalscoreText;
    public GameObject GameOverScreen;
   
    #endregion

    #region UI METHODS

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
        scoreText.text = ("Score: " + GameManager.Instance.PlayerEntity.playerScore);
    }

    public void ToggleGameOverScreen()
    {
        finalscoreText.text = "Final Score: "+GameManager.Instance.PlayerEntity.playerScore;
        finaldistanceText.text= "Distance Travelled: "+GameManager.Instance.PlayerEntity.playerDistance +"m";
        finalcoinText.text = "Total Coins: "+GameManager.Instance.PlayerEntity.playerCoinCount;

        GameOverScreen.SetActive(true);
    }

    #endregion
    
}
