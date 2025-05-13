// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public float hp;
    public float xp;
    public List<string> checkPoint;
    public List<Item> inventory;

    public PlayerData()
    {
        name = "Player";
        level = 1;
        hp = 100f;
        xp = 0f;
        checkPoint = new List<string>();
        inventory = new List<Item>();
    }
}