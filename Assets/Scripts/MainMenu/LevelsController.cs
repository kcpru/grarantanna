using UnityEngine;
using Txt = TMPro.TextMeshProUGUI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private Txt coinsCountUI;

    private void Start()
    {
        coinsCountUI.text = LevelsManager.Manager.CoinsCount.ToString();
    }
}
