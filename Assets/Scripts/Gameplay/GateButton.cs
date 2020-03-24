using UnityEngine;

public class GateButton : MonoBehaviour
{
    [SerializeField] private Gate gate;

    private GameObject normal;
    private GameObject pressed;

    private void Start()
    {
        normal = transform.GetChild(0).gameObject;
        pressed = transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Object"))
        {
            gate.Open();
            normal.SetActive(false);
            pressed.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gate.Close();
        normal.SetActive(true);
        pressed.SetActive(false);
    }
}
