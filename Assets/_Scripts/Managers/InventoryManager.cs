// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<GridSlot> items;
    private List<GridSlot> weapons;

    [SerializeField] private List<string> itemList;
    [SerializeField] private List<string> weaponList;

    void Awake()
    {
        // Initialize the lists
        items = new List<GridSlot>();
        weapons = new List<GridSlot>();

        itemList = new List<string>();
        weaponList = new List<string>();

        // Find all GridSlot components in the scene
        GridSlot[] allGridSlots = FindObjectsOfType<GridSlot>();

        foreach (GridSlot slot in allGridSlots)
        {
            if (slot.isWeapon)
            {
                weapons.Add(slot);
                weaponList.Add(slot.slotName);
            }
            else
            {
                items.Add(slot);
                itemList.Add(slot.slotName);
            }
        }
    }

    public void AddItem(string type, string itemName)
    {
        List<GridSlot> targetList = type == "weapon" ? weapons : items;
        List<string> targetNameList = type == "weapon" ? weaponList : itemList;

        foreach (GridSlot slot in targetList)
        {
            if (slot.isEmpty)
            {
                slot.slotName = itemName;
                slot.isEmpty = false;
                targetNameList.Add(itemName);
                break;
            }
        }
    }

    public void RemoveItem(GridSlot itemToRemove)
    {
        List<GridSlot> targetList = itemToRemove.isWeapon ? weapons : items;
        List<string> targetNameList = itemToRemove.isWeapon ? weaponList : itemList;

        foreach (GridSlot slot in targetList)
        {
            if (slot.row == itemToRemove.row && slot.column == itemToRemove.column)
            {
                targetNameList.Remove(slot.slotName);
                slot.slotName = "none";
                slot.isEmpty = true;
                break;
            }
        }
    }
}
