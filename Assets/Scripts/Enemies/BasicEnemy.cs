using UnityEngine;

///<summary>
///A basic enemy that deals damage to the player on contact. Can be used for spikes or as a base class for another enemy.
///</summary>
public class BasicEnemy : MonoBehaviour
{
    [Header("Damage")]
    public int Damage = 20;

    private PlayerHealth health;

    private void Start() {
        health = GameObject.Find("Health").GetComponent<PlayerHealth>();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.collider.name == "Player") {
            health.DamagePlayer(Damage);
        }
    }
}
