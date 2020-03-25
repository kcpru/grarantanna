using UnityEngine;

public class Key : MonoBehaviour
{
    private DoorManager doorManager;

    private void Awake() => doorManager = GameObject.FindGameObjectWithTag("Door").GetComponent<DoorManager>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SoundsManager.CurrentManager.PlaySound(SoundsManager.KEY_SOUND);
            doorManager.GetKey();
            Destroy(gameObject);
        }
    }
}
