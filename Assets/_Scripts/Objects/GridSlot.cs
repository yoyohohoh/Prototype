// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    public string slotName;
    public int row;
    public int column;
    public bool isEmpty = true;
    public bool isWeapon;
    public Sprite icon;
    public float hp;
    public GameObject weapon;

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
            PlayerController.Instance.PutWeapon(weapon);
            weapon.gameObject.SetActive(true);
        }
        else
        {
            PlayerController.Instance._playerData.hp += hp;
            InventoryManager.Instance.RemoveItem(this);
        }
    }
}
