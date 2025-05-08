using UnityEngine;
using System.Collections.Generic;

public class Temp : MonoBehaviour
{
    public bool isSave = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerData data = new PlayerData
        {
            name = "Player1",
            level = 1,
            hp = 100f,
            xp = 0f,
            checkPoint = new List<string> { "Checkpoint1", "Checkpoint2" }
        };
        GameSaveManager.Instance().SavePlayerData(data);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSave)
        {
            PlayerData data = GameSaveManager.Instance().LoadPlayerData();
            Debug.Log($"Name: {data.name}, Level: {data.level}, HP: {data.hp}, XP: {data.xp}");
            foreach (var checkpoint in data.checkPoint)
            {
                Debug.Log("Checkpoint: " + checkpoint);
            }
        }
    }
}
