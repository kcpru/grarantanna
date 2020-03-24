using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = PlayerController.CurrentPlayer.GetComponent<PlayerHealth>();

            if(playerHealth.CurrentHealth < playerHealth.maxHealth)
            {
                playerHealth.AddHealth(2);
                Destroy(gameObject);
            }
        }
    }
}
