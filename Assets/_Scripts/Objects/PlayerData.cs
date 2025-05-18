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
    public float hp;
    public float xp;
    public List<string> checkPoint;
    public List<GridSlot> inventory;

    public PlayerData()
    {
        name = "Yobisaboy";
        level = 1;
        hp = 100f;
        xp = 0f;
        checkPoint = new List<string>();
        inventory = new List<GridSlot>();
    }
    public float maxXp => level * 100f;
    // if hp greater than 100 return 100, if hp less than 0, return 0


}