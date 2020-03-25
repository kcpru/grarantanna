using UnityEngine;

public class GateButton : MonoBehaviour
{
    [SerializeField] private Gate gate;

    private GameObject normal;
    private GameObject pressed;

    private bool isOpened = false;

    private bool isPlayer = false;
    private bool isObject = false;

    private void Start()
    {
        normal = transform.GetChild(0).gameObject;
        pressed = transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Object"))
        {
            if (collision.CompareTag("Player"))
                isPlayer = true;

            if (collision.CompareTag("Object"))
                isObject = true;

            isOpened = true;
            gate.Open();
            normal.SetActive(false);
            pressed.SetActive(true);
            SoundsManager.CurrentManager.PlaySound(SoundsManager.GATE_SOUND);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayer = false;

        if (collision.CompareTag("Object"))
            isObject = false;

        if (!isPlayer && !isObject)
        {
            isOpened = false;
            gate.Close();
            normal.SetActive(true);
            pressed.SetActive(false);
            SoundsManager.CurrentManager.PlaySound(SoundsManager.GATE_SOUND);
        }
    }
}
