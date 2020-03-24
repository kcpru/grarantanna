using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject creditsPage;

    private void Start() => OpenPage("main");

    public void OpenPage (string pageName)
    {
        mainPage.SetActive(false);
        levelsPage.SetActive(false);
        creditsPage.SetActive(false);

        switch(pageName)
        {
            case "main":
                mainPage.SetActive(true);
                break;
            case "levels":
                levelsPage.SetActive(true);
                break;
            case "credits":
                creditsPage.SetActive(true);
                break;
        }
    }

    public void StartLevel(int levelIndex) => SceneManager.LoadScene(levelIndex);

    public void ExitGame ()
    {
        if(Application.isEditor)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Application.Quit(0);
        }
    }
}
