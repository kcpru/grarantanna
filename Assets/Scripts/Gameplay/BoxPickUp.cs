using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickUp : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.CompareTag("Object"))
        {
            if(Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("a");
                PlayerController.CurrentPlayer.GetComponent<PlayerController>().PickUpBox(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            Debug.Log("a");
        }
    }

}
