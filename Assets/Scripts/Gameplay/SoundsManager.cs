using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource healthSound;

    public static SoundsManager CurrentManager { get; private set; }

    #region Sounds
    public const string COIN_SOUND = "coin";
    public const string HEART_SOUND = "heart";
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
        }
    }
}
