using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [Header("Kill Plane Height")]
    public float KillPlaneHeight = -26f;

    private PlayerController pc;
    private PlayerHealth ph;

    private void Start()
    {
        pc = PlayerController.CurrentPlayer.GetComponent<PlayerController>();
        ph = PlayerController.CurrentPlayer.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(pc.transform.position.y < KillPlaneHeight) 
        {
            ph.DamagePlayer(99, true);
        }
    }

    private void OnDrawGizmos()
    {
        Color startColor = Gizmos.color;
        Gizmos.color = Color.red;

        Vector3 startPos = new Vector3(-99999f, KillPlaneHeight, transform.position.z);
        Vector3 endPos = new Vector3(99999f, KillPlaneHeight, transform.position.z);

        Gizmos.DrawLine(startPos, endPos);

        Gizmos.color = startColor;
    }
}
