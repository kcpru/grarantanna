using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    [Header("Trampoline force")]
    public float bounceForce = 70f;

    void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.collider.CompareTag("Player"))
        {
            foreach(ContactPoint2D point in col.contacts)
            {
                if (point.normal.y < 0)
                {
                    Rigidbody2D rb = PlayerController.CurrentPlayer.GetComponent<Rigidbody2D>(); 
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(new Vector2(0, 1) * bounceForce, ForceMode2D.Impulse);
                    SoundsManager.CurrentManager.PlaySound(SoundsManager.TRAMPOLINE_SOUND);

                }
            }
        }
    }
}
