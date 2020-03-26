using UnityEngine;
using Txt = TMPro.TextMeshProUGUI;
using System.Collections;

public class TutorialBrain : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private Txt txt;
    [SerializeField] private float delayEachLetter = 0.05f;

    private PlayerController playerController;
    private WeaponManager weaponManager;

    public static TutorialBrain CurrentTutorial { get; private set; }

    public bool IsTextDisplayed { get; private set; } = false;
    private int textIndex = 0;

    private IEnumerator coroutine;

    private readonly string[] texts = new string[]
    {
        "Witaj w przysłowiowej platformówce! W tej prostej grze chodzi o to, aby dostać się do drzwi. Jednak, abyś był w stanie je otworzyć, potrzebujesz zdobyć do nich klucz. Ale po koleji... Do poruszania się użyj AD lub strzałek na klawiaturze. Aby biec szybciej, przytrzymaj lewy lub prawy Shift. Jeśli spotkasz na swojej drodze przeciwników, uderz ich mieczem za pomocą lewego lub prawego Controla lub klawisza Alt",
        "Dobrze! Jeśli chcesz podskoczyć, naciśnij W lub Spację. Spróbuj.",
        "Jak widzisz poruszanie się jest bardzo proste. Teraz pora na coś bardziej zaawansowanego. W naszej grze znajduje się kilka przedmiotów, które mają za zadanie urozmaicić rozgrywkę. Stoisz własnie przed paroma z nich. Przycisk, który widzisz można aktywować stając na nim lub wpychając na niego jakiś przedmiot. Wówczas otworzy on jakąś bramę. Przedmioty możesz też podnosić stojąc przy nich i klikając E.",
        "Od przejścia pierwszego poziomu, dzieli Cie już tylko kilka kroków. Widzisz przed sobą drzwi, jednak aby je otworzyć potrzebujesz do nich klucza. Podnieś go pochodząc do niego.",
        "Brawo! Teraz wystarczy podejść do drzwi i nacisnąć klawisz E. Gratulacje, za chwile ukończysz swój pierwszy poziom w naszej grze!"
    };

    private void Awake() => CurrentTutorial = this;

    private void Start()
    {
        playerController = PlayerController.CurrentPlayer.GetComponent<PlayerController>();
        weaponManager = playerController.GetComponent<WeaponManager>();
        StartCoroutine(Begin());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(IsTextDisplayed)
            {
                if(coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                    txt.text = texts[textIndex - 1];
                }
                else
                {
                    tutorialUI.SetActive(false);
                    IsTextDisplayed = false;
                    playerController.canMove = true;
                    weaponManager.canPlayerHit = true;
                    txt.text = string.Empty;
                    PauseScreen.PauseMenu.canPause = true;
                }
            }
        }
    }

    private IEnumerator Begin ()
    {
        playerController.canMove = false;
        weaponManager.canPlayerHit = false;
        playerController.GetComponent<Animator>().SetBool("isGrounded", true);
        playerController.GetComponent<Animator>().SetBool("land", true);
        yield return new WaitForSecondsRealtime(2);
        DisplayText();
    }

    public void DisplayText()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = TextDisplayer();
        StartCoroutine(coroutine);
    }

    private IEnumerator TextDisplayer ()
    {
        PauseScreen.PauseMenu.canPause = false;

        playerController.canMove = false;
        weaponManager.canPlayerHit = false;
        playerController.GetComponent<Animator>().SetBool("isGrounded", true);
        playerController.GetComponent<Animator>().SetBool("land", true);
        playerController.GetComponent<Animator>().SetBool("walk", false);
        playerController.GetComponent<Animator>().SetBool("sprint", false);

        tutorialUI.SetActive(true);
        string text = texts[textIndex];

        textIndex++;

        IsTextDisplayed = true;
        txt.text = string.Empty;

        for (int i = 0; i <= text.Length; i++)
        {
            txt.text = text.Substring(0, i);
            yield return new WaitForSecondsRealtime(delayEachLetter);
        }

        coroutine = null;
        PauseScreen.PauseMenu.canPause = true;
    }
}
