using UnityEngine;
using Txt = TMPro.TextMeshProUGUI;

public class TimerLose : MonoBehaviour
{
    [SerializeField] private float time = 30f;
    [SerializeField] private Txt timerTxt;

    private float timer = 0f;

    private void Update()
    {
        if (timer <= time)
            timer += Time.deltaTime;

        timerTxt.text = Mathf.Round((time - timer)).ToString();

        if(timer >= time)
        {
            PlayerController.CurrentPlayer.GetComponent<PlayerHealth>().DamagePlayer(1000);
            timerTxt.text = "0";
            Destroy(this);
        }
    }
}
