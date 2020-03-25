using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text;

/// <summary>
/// Class to manage all player's progress, current unlocked levels etc. It is responsible for save and load progress as well.
/// </summary>
public class LevelsManager : MonoBehaviour
{
    /// <summary>
    /// Current manager.
    /// </summary>
    public static LevelsManager Manager;

    /// <summary>
    /// Localization where file with progress will be saved in.
    /// </summary>
    public string SavingPath { get; private set; }

    public int CoinsCount { get; private set; } = 0;

    private void Awake()
    {
        Manager = this;
        DontDestroyOnLoad(gameObject);

        SavingPath = Path.Combine(Application.dataPath, "GameSave.sav");
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

        File.WriteAllLines(SavingPath, textToSave);
    }

    /// <summary>
    /// Loads progress from file.
    /// </summary>
    public void LoadProgress ()
    {
        if(File.Exists(SavingPath))
        {
            string[] loadedLines = File.ReadAllLines(SavingPath);

            for (int i = 0; i < loadedLines.Length; i++)
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(loadedLines[i]);
                loadedLines[i] = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            try
            {
                CoinsCount = int.Parse(loadedLines[0].Remove(0, 6));
            }
            catch
            {
                Debug.LogError("Cannot parse values from file.", this);
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
    public void AddCoins(int coinsToAdd) => CoinsCount += coinsToAdd;

    /// <summary>
    /// Removes file with saved progress.
    /// </summary>
    public void ClearSave ()
    {
        if(File.Exists(SavingPath))
            File.Delete(SavingPath);
    }
}