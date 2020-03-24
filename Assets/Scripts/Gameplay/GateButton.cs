using UnityEngine;

public class GateButton : MonoBehaviour
{
    [SerializeField] private Gate gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Object"))
        {
            gate.Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gate.Close();
    }
}
