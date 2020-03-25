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
    public float bounceForce = 30f;
    public bool ignoreInvicibility = false;

    [Header("Damage")]
    public int damage = 2;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            foreach (ContactPoint2D point in col.contacts)
            {
                if (point.normal.y < 0)
                {
                    if(!canBouncePlayer)
                        PlayerController.CurrentPlayer.GetComponent<PlayerController>().GetHit();
                }
                else
                {
                    PlayerController.CurrentPlayer.GetComponent<PlayerController>().GetHit();
                }
            }
        }
    
        EveryCollision(col);
    }
    
    void OnCollisionStay2D(Collision2D col)
    {
        EveryCollision(col);
    }

    ///<summary>
    ///Should be called in OnCollisionEnter and OnCollisionStay
    ///</summary>
    private void EveryCollision(Collision2D col) 
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
                    if(!canBouncePlayer)
                    {
                        PlayerController.CurrentPlayer.GetComponent<PlayerHealth>().DamagePlayer(damage, ignoreInvicibility);
                    }
                }
                else
                {
                    PlayerController.CurrentPlayer.GetComponent<PlayerHealth>().DamagePlayer(damage, ignoreInvicibility);
                }
            }
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            print("killed enemy");
            SoundsManager.CurrentManager.PlaySound(SoundsManager.KILLED_ENEMY_SOUND);
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
