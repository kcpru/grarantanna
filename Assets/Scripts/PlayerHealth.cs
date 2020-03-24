using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;
    public int invincibilityFrames = 100; //Not actual frames, fixed update frames 

    [Header("UI Elements")]
    [SerializeField] private GameObject heartGO;
    [SerializeField] private Transform heartsRoot;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    public bool Invincible { get; private set; } = false;
    public int CurrentHealth { get; private set; } = 10;
    public bool IsAlive { get; private set; } = true;

    private int invincibilityFramesCounter;

    private List<GameObject> hearts = new List<GameObject>();
    private Vector2 heartsPos;
    private readonly Vector2 START_HEART_POS = new Vector2(-50, -50);

    private void Start()
    {
        DrawUI();
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
            CurrentHealth = 0;

        if (CurrentHealth <= 0 && IsAlive)
        {
            CurrentHealth = 0;
            KillPlayer();
        }

        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Checks if the player has died and handles invincibility frames 
    /// </summary>
    private void FixedUpdate()
    {
        if(Invincible)
        {
            invincibilityFramesCounter -= 1;
        }
        
        if(invincibilityFramesCounter <= 0)
        {
            Invincible = false;
        }
    }

    /// <summary>
    /// Called when the player dies
    /// </summary>
    private void KillPlayer()
    {
        IsAlive = false;
        GetComponent<PlayerController>().Death();
    }

    /// <summary>
    /// Damages the player without ignoring the invincibility frames(you can change the health directly if you want to bypass invincibility frames)
    /// </summary>
    public void DamagePlayer(int amount)
    {
        if(!Invincible)
        {
            if (CurrentHealth - amount <= 0)
                CurrentHealth = 0;
            else
                CurrentHealth -= amount;

            invincibilityFramesCounter = invincibilityFrames;
            Invincible = true;
            DrawUI();
        }
    }

    /// <summary>
    /// Draws hearts in the UI.
    /// </summary>
    public void DrawUI ()
    {
        heartsPos = START_HEART_POS;

        foreach(var go in hearts)
            Destroy(go);

        hearts.Clear();

        int heartsCount = maxHealth / 2;

        for (int i = 0; i < heartsCount; i++)
        {
            GameObject newHeart = Instantiate(heartGO, Vector3.zero, Quaternion.identity, heartsRoot);
            newHeart.GetComponent<RectTransform>().anchoredPosition = heartsPos;
            hearts.Add(newHeart);
            heartsPos += new Vector2(-60, 0);
        }

        if(CurrentHealth % 2 == 0)
        {
            int countOfEmptyHearts = (maxHealth - CurrentHealth) / 2;

            for(int i = hearts.Count - 1; i >= (hearts.Count - countOfEmptyHearts); i--)
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
        }
        else
        {
            int countOfEmptyHearts = (maxHealth - (CurrentHealth - 1)) / 2;

            for (int i = hearts.Count - 1; i >= (hearts.Count - countOfEmptyHearts); i--)
                hearts[i].GetComponent<Image>().sprite = emptyHeart;

            hearts[hearts.Count - countOfEmptyHearts].GetComponent<Image>().sprite = halfHeart;
        }
    }
}
