using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] private int countOfDoubleJumps = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController.CurrentPlayer.GetComponent<PlayerController>().AddDoubleJump(countOfDoubleJumps);
            Destroy(gameObject);
        }
    }
}
