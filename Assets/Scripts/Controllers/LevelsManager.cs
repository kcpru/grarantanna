using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text;
using Txt = TMPro.TextMeshProUGUI;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Class to manage all player's progress, current unlocked levels etc. It is responsible for save and load progress as well.
/// </summary>
public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Txt coinsCountUI;
    [SerializeField] private Level[] levels;

    /// <summary>
    /// Current manager.
    /// </summary>
    public static LevelsManager Manager;

    /// <summary>
    /// Localization where file with progress will be saved in.
    /// </summary>
    public string SavingPath { get; private set; }

    public int CoinsCount { get; private set; } = 0;
    private List<string> coinsFromLevels = new List<string>();
    private List<string> maxCoins = new List<string>();

    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        #if WINDOWS
            SavingPath = Path.Combine(Application.dataPath, "GameSave.sav");
        #else
            SavingPath = Path.Combine(Application.persistentDataPath, "GameSave.sav");
        #endif
        Debug.Log(SavingPath);
    }

    private void Start()
    {
        LoadProgress();
        coinsCountUI.text = CoinsCount.ToString();

        foreach (var level in levels)
        {
            if (level.coinsToUnlock <= CoinsCount)
                level.levelObject.GetComponent<Button>().interactable = true;
            else
                level.levelObject.GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    /// Indicates that is level with given index unlocked.
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public bool IsLevelUnlocked (int levelIndex)
    {
        if (levels[levelIndex - 1].coinsToUnlock <= CoinsCount)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Saves current progress to file.
    /// </summary>
    public void SaveProgress ()
    {
        string[] textToSave = new string[]
        {
            $"coins={CoinsCount.ToString()}",
        };

        for(int i = 0; i < textToSave.Length; i++)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textToSave[i]);
            textToSave[i] = Convert.ToBase64String(plainTextBytes);
        }

        //File.WriteAllLines(SavingPath, textToSave);

        List<string> toSave = new List<string>();
        for(int i = 0; i < textToSave.Length; i++)
        {
            toSave.Add(textToSave[i]);
        }

        for(int i = 0; i < coinsFromLevels.Count; i++)
        {
            toSave.Add(coinsFromLevels[i]);
            toSave.Add(maxCoins[i]);

            print($"coinsFromLevels = {coinsFromLevels[i]} maxCoins = {maxCoins[i]}");
        }
        
        PlayerPrefs.SetString("SaveData", String.Join("\n",toSave));
    }

    /// <summary>
    /// Loads progress from file.
    /// </summary>
    public void LoadProgress ()
    {
        string loadedData = PlayerPrefs.GetString("SaveData");
        if(loadedData != null)
        {
            string[] loadedLines = loadedData.Split('\n');

            byte[] base64EncodedBytes = Convert.FromBase64String(loadedLines[0]);
            loadedLines[0] = Encoding.UTF8.GetString(base64EncodedBytes);

            try
            {
                CoinsCount = int.Parse(loadedLines[0].Remove(0, 6));
            }
            catch
            {
                Debug.LogError("Cannot parse values from file.", this);
            }

            if(loadedLines.Length > 1)
            {
                coinsFromLevels.Clear();
                maxCoins.Clear();

                for(int i = 2; i < loadedLines.Length; i+=2)
                {
                    coinsFromLevels.Add(loadedLines[i]);
                    maxCoins.Add(loadedLines[i - 1]);
                }
            }
        }
    }

    /// <summary>
    /// Total count of all levels.
    /// </summary>
    public int LevelsCount => SceneManager.sceneCountInBuildSettings - 1;

    /// <summary>
    /// Adds coins to main balance.
    /// </summary>
    public void AddCoins(int coinsToAdd, int levelIndex)
    {
        if (!coinsFromLevels.Contains(levelIndex.ToString()))
        {
            CoinsCount += coinsToAdd;
            coinsFromLevels.Add(levelIndex.ToString());
            maxCoins.Add(coinsToAdd.ToString());
        }
        else
        {
            int dif = coinsToAdd - int.Parse(maxCoins[levelIndex - 1]);
            print(dif);
            if (dif > 0)
            {
                CoinsCount += dif;
                maxCoins[levelIndex - 1] = maxCoins.ToString();
            }
        }
    }

    /// <summary>
    /// Removes file with saved progress.
    /// </summary>
    public void ClearSave ()
    {
        if(File.Exists(SavingPath))
            File.Delete(SavingPath);
    }

    [Serializable]
    public struct Level
    {
        public GameObject levelObject;
        public int coinsToUnlock;
    }
}