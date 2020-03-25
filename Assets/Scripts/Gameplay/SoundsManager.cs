using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource healthSound;
    [SerializeField] private AudioSource trampolineSound;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource keySound;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource killedEnemySound;
    [SerializeField] private AudioSource gateSound;

    public static SoundsManager CurrentManager { get; private set; }

    #region Sounds
    public const string COIN_SOUND = "coin";
    public const string HEART_SOUND = "heart";
    public const string TRAMPOLINE_SOUND = "trampoline";
    public const string ATTACK_SOUND = "attack";
    public const string KEY_SOUND = "key";
    public const string DOOR_SOUND = "door";
    public const string KILLED_ENEMY_SOUND = "killed_enemy";
    public const string GATE_SOUND = "gate";
    #endregion

    private void Awake() => CurrentManager = this;

    /// <summary>
    /// Plays sound with given name.
    /// </summary>
    /// <param name="soundName"></param>
    public void PlaySound (string soundName)
    {
        switch(soundName)
        {
            case COIN_SOUND:
                coinSound.Play();
                break;
            case HEART_SOUND:
                healthSound.Play();
                break;
            case TRAMPOLINE_SOUND:
                trampolineSound.Play();
                break;
            case ATTACK_SOUND:
                attackSound.Play();
                break;
            case KEY_SOUND:
                keySound.Play();
                break;
            case DOOR_SOUND:
                doorSound.Play();
                break;
            case KILLED_ENEMY_SOUND:
                killedEnemySound.Play();
                break;
            case GATE_SOUND:
                gateSound.Play();
                break;
            default:
                break;
        }
    }
}
