using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerEntity : MonoBehaviour
{
    private int playerDistance;
    private int playerScore;
    private int playerCoinCount;
    private bool isAlive = true;
    
    
    public void Die()//todo: move this to entity too lmao.
    {
        isAlive = false;
        //then restart game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
