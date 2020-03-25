using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    [SerializeField] private Button nextLevelBtn;
    public static GameObject WinScreen { get; private set; }
    public static WinGame CurrentScreen;

    private void Awake()
    {
        CurrentScreen = this;
        WinScreen = gameObject;
        WinScreen.SetActive(false);
    }

    public void Win()
    {
        if(!LevelsManager.Manager.IsLevelUnlocked(SceneManager.GetActiveScene().buildIndex + 1))
        {
            nextLevelBtn.interactable = false;
        }

        WinScreen.SetActive(true);
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
