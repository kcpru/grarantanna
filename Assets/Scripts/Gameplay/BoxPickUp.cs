using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickUp : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col) 
    {
        if (col.CompareTag("Object"))
        {
            if(Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("a");
                PlayerController.CurrentPlayer.GetComponent<PlayerController>().PickUpBox(col.gameObject);
            }
        }
    }
}
