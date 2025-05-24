// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : PersistentSingleton<InventoryManager>
{
    public List<GridSlot> items;
    public List<GridSlot> weapons;

    [SerializeField] private List<string> itemList;
    [SerializeField] private List<string> weaponList;

    void Start()
    {
        // Initialize the lists
        items = new List<GridSlot>();
        weapons = new List<GridSlot>();

        // Find all GridSlot components in the scene
        GridSlot[] allGridSlots = Object
            .FindObjectsByType<GridSlot>(FindObjectsSortMode.None)
            .OrderBy(slot => slot.row)
            .ThenBy(slot => slot.column)
            .ToArray();

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

        List<GridSlotData> _playerDataInventory = GameSaveManager.Instance().LoadPlayerData().inventory;
        if (_playerDataInventory != null)
        {
            foreach (GridSlotData slotData in _playerDataInventory)
            {
                foreach (GridSlot gridSlot in allGridSlots)
                {
                    if (gridSlot.row == slotData.row && gridSlot.column == slotData.column && gridSlot.isWeapon == slotData.isWeapon)
                    {
                        gridSlot.slotName = slotData.slotName;
                        gridSlot.icon = slotData.icon;
                        gridSlot.isEmpty = slotData.isEmpty;
                        gridSlot.item = Resources.Load<GameObject>($"Prefabs/{slotData.itemName}");
                        if (!gridSlot.isEmpty && gridSlot.isWeapon)
                        {
                            GameObject weapon = Instantiate(gridSlot.item, new Vector3(0, 0, 0), Quaternion.identity);
                            weapon.gameObject.GetComponent<Weapon>().SetStatus(WeaponStatus.PickedUp);
                            gridSlot.item = weapon;
                        }
                    }
                }
            }
        }

        CopyToList();
    }

    void CopyToList()
    {
        itemList.Clear();
        weaponList.Clear();
        foreach (GridSlot slot in items)
        {
            itemList.Add(slot.slotName);
        }
        foreach (GridSlot slot in weapons)
        {
            weaponList.Add(slot.slotName);
        }
    }

    public void AddItem(GameObject itemObj)
    {
        List<GridSlot> targetList = new List<GridSlot>();
        string name = "";
        Sprite icon = null;

        if (itemObj.GetComponent<Item>())
        {
            Item item = itemObj.GetComponent<Item>();
            targetList = items;
            name = item._name;
            icon = item._icon;
            item.SetStatus(ItemStatus.PickedUp);
        }
        else if (itemObj.GetComponent<Weapon>())
        {
            Weapon weapon = itemObj.GetComponent<Weapon>();
            targetList = weapons;
            name = weapon._name;
            icon = weapon._icon;
            weapon.SetStatus(WeaponStatus.PickedUp);
        }

        foreach (GridSlot slot in targetList)
        {
            if (slot.isEmpty)
            {
                slot.slotName = name;
                slot.isEmpty = false;
                slot.icon = icon;
                slot.item = itemObj;
                CopyToList();
                break;
            }
        }
    }

    public void RemoveItem(GridSlot itemToRemove)
    {
        List<GridSlot> targetList = itemToRemove.isWeapon ? weapons : items;

        foreach (GridSlot slot in targetList)
        {
            if (slot.row == itemToRemove.row && slot.column == itemToRemove.column)
            {
                slot.slotName = "none";
                slot.isEmpty = true;
                CopyToList();
                break;
            }
        }
    }
}
