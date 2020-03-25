using UnityEngine;
using Txt = TMPro.TextMeshProUGUI;

public class CoinsCollector : MonoBehaviour
{
    public int currentCoinsCount = 0;
    public int coinsToCollect = 3;
    [Space]
    [SerializeField] private Txt coinsUI;
    [SerializeField] private Txt finalCoinsUI;

    public static CoinsCollector Collector { get; private set; }

    private void Awake()
    {
        Collector = this;
        var coins = FindObjectsOfType<Coin>();
        coinsToCollect = coins != null ? coins.Length : 0;
        coinsUI.text = "0/" + coinsToCollect.ToString();
        finalCoinsUI.text = "0/" + coinsToCollect.ToString();
    }

    public void CollectCoin ()
    {
        if (currentCoinsCount < coinsToCollect)
        {
            currentCoinsCount++;
            coinsUI.text = currentCoinsCount.ToString() + "/" + coinsToCollect.ToString();
            finalCoinsUI.text = currentCoinsCount.ToString() + "/" + coinsToCollect.ToString();
        }
    }
}
