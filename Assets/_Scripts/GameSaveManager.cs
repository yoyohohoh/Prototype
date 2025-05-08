// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
using System.IO;

public class GameSaveManager
{
    private static GameSaveManager m_instance = null;
    private GameSaveManager() { }
    public static GameSaveManager Instance()
    {
        return m_instance ??= new GameSaveManager();
    }

    string path;
    public void SavePlayerData(PlayerData data)
    {
        string timeStamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string json = JsonUtility.ToJson(data);
        path = $"{Application.persistentDataPath}/{timeStamp}.json";

        File.WriteAllText(path, json);
        Debug.Log("Player data saved to " + path);
    }
    public PlayerData LoadPlayerData()
    {
        string json = File.ReadAllText(path);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        return data;
    }
}
