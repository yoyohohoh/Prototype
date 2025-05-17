// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;

public enum ItemStatus
{
    NotAvailable,
    Available,
    PickedUp,
    Used
}

public class Item : MonoBehaviour
{
    public ItemStatus _status;

    public int _id;
    public string _name;
    public string _description;
    public Sprite _icon;

    public float _hp;
    public float _xp;
}
