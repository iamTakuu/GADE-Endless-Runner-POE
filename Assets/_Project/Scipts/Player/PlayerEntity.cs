using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerEntity : MonoBehaviour
{
    public int playerDistance;
    public int playerScore;
    private float origin;
    public int playerCoinCount;
    private bool isAlive = true;

    private void Start()
    {
        origin = transform.position.z;
    }

    public void Die()
    {
        isAlive = false;
        //then restart game
    }

    
    public bool IsAlive()
    {
        return isAlive;
    }

    public void CollectCoin()
    {
        playerCoinCount++;
    }

    public void IncrementScore()
    {
        playerScore++;
    }

    private void Update()
    {
        playerDistance = Mathf.RoundToInt(transform.position.z - origin)/4;
    }
}
