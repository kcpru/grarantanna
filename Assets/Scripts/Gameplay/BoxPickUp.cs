using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickUp : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision) 
    {
        Debug.Log("a");
        if (collision.CompareTag("Object"))
        {
            if(Input.GetKeyUp(KeyCode.E))
            {
                PlayerController.CurrentPlayer.GetComponent<PlayerController>().PickUpBox(collision.gameObject);
            }
        }
    }
}
