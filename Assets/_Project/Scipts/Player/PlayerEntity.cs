using System;
using Cinemachine;
using UnityEngine;


public class PlayerEntity : MonoBehaviour
{
    #region VARIABLES
    
    public int playerDistance;
    public int obstacleScore;
    public int playerScore;
    private float origin;
    public int playerCoinCount;
    private bool isMagnetised;
    private bool isShielded;
    public CinemachineVirtualCamera DeathCam;

    //public Leaderboard Leaderboard;
    #endregion
    
    #region CUSTOM METHODS
    public void Magnetise() => isMagnetised = true;
    public void DeMagnetise() => isMagnetised = false;

    public void Shield() => isShielded = true;

    public void UnShield() => isShielded = false;
    //public void Die() => isAlive = false;
    public void CollectCoin() => playerCoinCount++;
    public void IncrementScore() => obstacleScore++;
   
    // public bool IsAlive()
    // {
    //     return isAlive;
    // }
    public bool IsMagnetised()
    {
        return isMagnetised;
    }

    public bool IsShielded()
    {
        return isShielded;
    }

    private void EnableDeathCam()
    {
       DeathCam.enabled = true;
    }

    private void CalculateFinalScore()
    {
        playerScore += playerDistance;
        playerScore += playerCoinCount * 5;
        playerScore += obstacleScore * 10;

    }
    
    #endregion
    
    #region UNITY METHODS

    // private void Awake()
    // {
    //     Leaderboard = gameObject.
    // }

    private void OnEnable()
    {
        EventsManager.Instance.PlayerDeath += EnableDeathCam;
        EventsManager.Instance.PlayerDeath += CalculateFinalScore;
        EventsManager.Instance.PassObstacle += IncrementScore;
    }

    private void OnDisable()
    {
        EventsManager.Instance.PlayerDeath -= EnableDeathCam;
        EventsManager.Instance.PlayerDeath -= CalculateFinalScore;

        EventsManager.Instance.PassObstacle -= IncrementScore;
    }
    
    private void Start()
    {
        origin = transform.position.z;
        playerScore = 0;
    }
    
    private void Update()
    {
        playerDistance = Mathf.RoundToInt(transform.position.z - origin)/4;
    }

    #endregion
    
}
