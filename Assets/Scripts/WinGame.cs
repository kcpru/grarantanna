using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public static GameObject WinScreen { get; private set; }

    private void Awake()
    {
        WinScreen = gameObject;
        WinScreen.SetActive(false);
    }

    public static void Win() //Should probably be defined on the deathscren object but I'm tired
    {
        WinScreen.SetActive(true);
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene(0);
    }

}
