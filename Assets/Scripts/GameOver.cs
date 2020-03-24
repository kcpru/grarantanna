using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{  
    public static GameObject DeathScreen {get; private set; }

    private void Awake() 
    {
        DeathScreen = gameObject;
        DeathScreen.SetActive(false);
    }

    public static void EndGame() //Should probably be defined on the deathscren object but I'm tired
    {
        DeathScreen.SetActive(true);
    }

    public void OnRestartButtonPress() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButtonPress() 
    {
        SceneManager.LoadScene(0);
    }

}
