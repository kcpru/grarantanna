using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int invincibilityFrames = 100; //Not actual frames, fixed update frames 


    public int Health {get; set;}
    public bool Invincible {get; private set;} = false;
    private int invincibilityFramesCounter;

    private void Start()
    {
        Health = maxHealth;
    }

    /// <summary>
    /// Checks if the player has died and handles invincibility frames 
    /// </summary>
    private void FixedUpdate()
    {
        if(Health <= 0) {
            KillPlayer();
        }

        if(Invincible) {
            invincibilityFramesCounter -= 1;
        }
        
        if(invincibilityFramesCounter <= 0) {
            Invincible = false;
        }
    }

    /// <summary>
    /// Called when the player dies
    /// </summary>
    private void KillPlayer() {
        //TODO
    }

    /// <summary>
    /// Damages the player without ignoring the invincibility frames(you can change the health directly if you want to bypass invincibility frames)
    /// </summary>
    public void DamagePlayer(int amount) {
        if(!Invincible) {
            Health -= amount;
            invincibilityFramesCounter = invincibilityFrames;
            Invincible = true;
        }
    }
}
