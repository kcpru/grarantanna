using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CoinsCollector.Collector.CollectCoin();
            SoundsManager.CurrentManager.PlaySound(SoundsManager.COIN_SOUND);
            Destroy(gameObject);
        }
    }
}
