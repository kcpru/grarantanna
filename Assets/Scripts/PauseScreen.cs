using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{  
    public static GameObject Screen {get; private set; }

    private float savedTimeScale = 1f;
    private bool savedCanMove = true;
    
    private void Start() 
    {
        Screen = gameObject;
        Screen.SetActive(false);
    }

    public void pause() 
    {
        Screen.SetActive(true);
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        PlayerController pc = PlayerController.CurrentPlayer.GetComponent<PlayerController>();
        savedCanMove = pc.canMove;
        pc.canMove = false;
    }

    public void unpause() 
    {
        Time.timeScale = savedTimeScale;
        PlayerController.CurrentPlayer.GetComponent<PlayerController>().canMove = savedCanMove;
        Screen.SetActive(false);
    }

    private void OnDestroy() {
        unpause();
    }

    public void OnRestartButtonPress() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButtonPress() 
    {
        SceneManager.LoadScene(0);
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            unpause();
        }  
    }

}
