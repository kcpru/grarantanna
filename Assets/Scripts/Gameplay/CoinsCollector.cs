using UnityEngine;
using Txt = TMPro.TextMeshProUGUI;

public class CoinsCollector : MonoBehaviour
{
    public int currentCoinsCount = 0;
    public int coinsToCollect = 3;
    [Space]
    [SerializeField] private Txt coinsUI;

    public static CoinsCollector Collector { get; private set; }

    private void Awake()
    {
        Collector = this;
        coinsUI.text = "0/" + coinsToCollect.ToString();
    }

    public void CollectCoin ()
    {
        if (currentCoinsCount < coinsToCollect)
        {
            currentCoinsCount++;
            coinsUI.text = currentCoinsCount.ToString() + "/" + coinsToCollect.ToString();
        }
    }
}
