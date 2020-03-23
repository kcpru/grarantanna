using UnityEngine;

///<summary>
///A basic enemy that deals damage to the player on contact. Can be used for spikes or as a base class for another enemy.
///</summary>
public class BasicEnemy : MonoBehaviour
{
    [Header("Enemy settings")]
    public int health = 2;
    public bool killable = true;
    public bool canBouncePlayer = true;

    [Header("Damage")]
    public int damage = 2;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
            foreach(ContactPoint2D point in col.contacts)
            {
                if (point.normal.y < 0)
                {
                    PlayerController.CurrentPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * 30f, ForceMode2D.Impulse);

                    if(!canBouncePlayer)
                    {
                        PlayerController.CurrentPlayer.GetComponent<PlayerHealth>().DamagePlayer(damage);
                    }
                }
            }
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            print("killed enemy");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gives damage to this enemy.
    /// </summary>
    /// <param name="damage"></param>
    public void GiveDamage(int damage)
    {
        if (killable)
        {
            health -= damage;
        }
    }
}
