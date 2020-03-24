using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource coinSound;

    public static SoundsManager CurrentManager { get; private set; }

    #region Sounds
    public const string COIN_SOUND = "coin";
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
        }
    }
}
