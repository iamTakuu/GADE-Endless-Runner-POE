using Cinemachine;
using UnityEngine;


public class PlayerEntity : MonoBehaviour
{
    public int playerDistance;
    public int playerScore;
    private float origin;
    public int playerCoinCount;
    //private bool isAlive = true;
    private bool isMagnetised;
    //public State playerState;
    public CinemachineVirtualCamera DeathCam;


    private void OnEnable()
    {
        EventsManager.Instance.PlayerDeath += EnableDeathCam;
    }

    private void OnDisable()
    {
        EventsManager.Instance.PlayerDeath -= EnableDeathCam;

    }

    #region CUSTOM METHODS
    public void Magnetise() => isMagnetised = true;
    public void DeMagnetise() => isMagnetised = false;
    //public void Die() => isAlive = false;
    public void CollectCoin() => playerCoinCount++;
    public void IncrementScore() => playerScore++;
   
    // public bool IsAlive()
    // {
    //     return isAlive;
    // }
    public bool IsMagnetised()
    {
        return isMagnetised;
    }

    private void EnableDeathCam()
    {
       DeathCam.enabled = true;
    }
    
    #endregion
    

    


    #region UNITY METHODS

    private void Start()
    {
        origin = transform.position.z;
      
    }
    private void Update()
    {
        playerDistance = Mathf.RoundToInt(transform.position.z - origin)/4;
    }

    #endregion
    
    
    
    
}
