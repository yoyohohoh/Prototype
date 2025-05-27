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
    string pathFolder = $"{Application.persistentDataPath}/";
    public void SavePlayerData(PlayerData data)
    {
        foreach(GridSlot slot in InventoryManager.Instance.GetList("items"))
        {
            GridSlotData slotData = new GridSlotData
            {
                slotName = slot.slotName,
                icon = slot.icon,
                row = slot.row,
                column = slot.column,
                isEmpty = slot.isEmpty,
                isWeapon = slot.isWeapon,
                itemName = slot.item != null ? slot.item.name : null
            };
            data.inventory.Add(slotData);
        }

        foreach (GridSlot slot in InventoryManager.Instance.GetList("weapons"))
        {
            GridSlotData slotData = new GridSlotData
            {
                slotName = slot.slotName,
                icon = slot.icon,
                row = slot.row,
                column = slot.column,
                isEmpty = slot.isEmpty,
                isWeapon = slot.isWeapon,
                itemName = slot.item != null ? slot.item.name : null
            };
            data.inventory.Add(slotData);
        }

        string timeStamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string json = JsonUtility.ToJson(data);
        path = $"{pathFolder}/{timeStamp}.json";

        File.WriteAllText(path, json);
        Debug.Log("Player data saved to " + path);
    }
    public PlayerData LoadPlayerData()
    {
        string[] files = Directory.GetFiles(pathFolder, "*.json");
        if (files.Length > 0)
        {
            path = files[files.Length - 1];
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        else
        {
            Debug.Log("No save file found.");
            return null;
        }
    }

    public void DeleteSaveData()
    {
        string[] files = Directory.GetFiles(pathFolder, "*.json");

        if (files.Length > 0)
        {
            foreach (string file in files)
            {
                File.Delete(file);
                Debug.Log("Deleted save file: " + file);
            }
        }
        else
        {
            Debug.Log("No save file found to delete.");
        }
    }

}
