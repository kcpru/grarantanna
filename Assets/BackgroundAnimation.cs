using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.position.x > 20) {
            transform.position = new Vector2(-18f,transform.position.y);
        }
        transform.Translate(0.01f,0,0);
    }
}
