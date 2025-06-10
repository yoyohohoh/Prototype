// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public float gold;
    public float hp;
    public float xp;
    public List<string> checkPoints;
    public List<string> npcKilled;
    public List<GridSlotData> inventory;


    public PlayerData()
    {
        name = "Yobisaboy";
        level = 1;
        gold = 0f;
        hp = 100f;
        xp = 0f;
        checkPoints = new List<string>();
        npcKilled = new List<string>();
        inventory = new List<GridSlotData>();
    }
    public float maxXp => level * 100f;

};



