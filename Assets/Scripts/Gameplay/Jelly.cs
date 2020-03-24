using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private float doubleJumpTime = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController.CurrentPlayer.GetComponent<PlayerController>().AddDoubleJumpTime(doubleJumpTime);
            Destroy(gameObject);
        }
    }
}
