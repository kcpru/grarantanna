using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BasicEnemy>().GiveDamage(PlayerController.CurrentPlayer.GetComponent<PlayerController>().damage);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
