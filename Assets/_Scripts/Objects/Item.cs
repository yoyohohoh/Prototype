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
    public ItemStatus _itemStatus;

    public int _itemID;
    public string _itemName;
    public string _itemDescription;

    public float _itemHp;
    public float _itemXp;
}
