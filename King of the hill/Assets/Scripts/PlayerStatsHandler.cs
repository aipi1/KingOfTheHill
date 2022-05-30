using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// Transfers playerName between scenes using Singleton pattern, handles
/// saving and loading best score
/// </summary>
public class PlayerStatsHandler : MonoBehaviour
{
    public static PlayerStatsHandler Instance { get; private set; }
    public string BestName { get; private set; }
    public int BestScore { get; private set; }
    public string playerName = "";
    public int score = 0;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public string playerName;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.bestScore = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/koth_savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/koth_savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestName = data.playerName;
            BestScore = data.bestScore;
        }
    }
}
