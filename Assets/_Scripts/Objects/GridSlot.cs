// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GridSlotData
{
    public string slotName;
    public Sprite icon;
    public int row;
    public int column;
    public bool isEmpty = true;
    public bool isWeapon;
    public string itemName;
}

public class GridSlot : MonoBehaviour
{
    public string slotName;
    public Sprite icon;
    public int row;
    public int column;
    public bool isEmpty = true;
    public bool isWeapon;

    public GameObject item;


    void Update()
    {
        Image thisImage = this.GetComponent<Image>();
        if (thisImage != null)
        {
            thisImage.sprite = icon;
            thisImage.color = isEmpty ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        }
    }

    public void RemoveGridSlot()
    {
        if (isWeapon)
        {
            item.GetComponent<Weapon>().SetStatus(WeaponStatus.Equipped);
        }
        else
        {
            item.GetComponent<Item>().SetStatus(ItemStatus.Used);
            InventoryManager.Instance.RemoveItem(this);
        }
    }
}
