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

        // MGOT
        if(item != null)
        {
            if (item.GetComponentInChildren<WeaponMaterialModifier>())
            {
                thisImage.color = item.GetComponentInChildren<WeaponMaterialModifier>().GetColorByName();
            }
        }
        
    }

    public void RemoveGridSlot()
    {
        if (isWeapon)
        {
            foreach (GridSlot gridSlot in InventoryManager.Instance.GetList(GridSlotType.Weapon))
            {
                if (!gridSlot.isEmpty)
                {
                    gridSlot.item.GetComponent<Weapon>().SetStatus(WeaponStatus.PickedUp);
                }
            }
            item.GetComponent<Weapon>().SetStatus(WeaponStatus.Equipped);
        }
        else
        {
            item.GetComponent<Item>().SetStatus(ItemStatus.Used);
            InventoryManager.Instance.RemoveItem(this);
        }
    }
}
