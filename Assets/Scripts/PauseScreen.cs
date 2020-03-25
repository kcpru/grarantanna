using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public bool canPause = true;
    public static GameObject Screen { get; private set; }
    public static PauseScreen PauseMenu { get; private set; }

    private float savedTimeScale = 1f;
    private bool savedCanMove = true;

    private void Awake() => PauseMenu = this;

    private void Start() 
    {
        Screen = gameObject;
        Screen.SetActive(false);
    }

    public void Pause() 
    {
        if (canPause)
        {
            Screen.SetActive(true);
            savedTimeScale = Time.timeScale;
            Time.timeScale = 0;
            PlayerController pc = PlayerController.CurrentPlayer.GetComponent<PlayerController>();
            savedCanMove = pc.canMove;
            pc.canMove = false;
        }
    }

    public void Unpause() 
    {
        Time.timeScale = savedTimeScale;

        if (PlayerController.CurrentPlayer != null)
            PlayerController.CurrentPlayer.GetComponent<PlayerController>().canMove = savedCanMove;

        Screen.SetActive(false);
    }

    private void OnDestroy()
    {
        Unpause();
    }

    public void OnRestartButtonPress() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButtonPress() 
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            Unpause();
        }  
    }

}
