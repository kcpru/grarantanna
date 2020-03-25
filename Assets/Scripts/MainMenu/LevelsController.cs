using UnityEngine;
using UnityEngine.UI;
using Txt = TMPro.TextMeshProUGUI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private Txt coinsCountUI;
    [SerializeField] private Level[] levels;

    private void Start()
    {
        LevelsManager.Manager.LoadProgress();
        coinsCountUI.text = LevelsManager.Manager.CoinsCount.ToString();

        foreach (var level in levels)
        {
            if (level.coinsToUnlock <= LevelsManager.Manager.CoinsCount)
                level.levelObject.GetComponent<Button>().interactable = true;
            else
                level.levelObject.GetComponent<Button>().interactable = false;
        }
    }

    [System.Serializable]
    public struct Level
    {
        public GameObject levelObject;
        public int coinsToUnlock;
    }
}
