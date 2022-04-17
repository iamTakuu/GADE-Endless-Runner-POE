using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Text Mesh Elements")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;



    

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
    
}
