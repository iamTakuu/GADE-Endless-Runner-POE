using UnityEngine;

public class Coin : MonoBehaviour
{
    
    
    
    public float rotationSpeed = 30f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) //If the coin collides with an obstacle
        {
            Destroy(gameObject);
            return;
        }
        //Debug.Log("BLING");
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.PlayerEntity.CollectCoin();
        
        
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
