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

    public void SetStatus(ItemStatus newStatus)
    {
        switch(newStatus)
        {
            case ItemStatus.NotAvailable:
                _status = ItemStatus.NotAvailable;
                gameObject.SetActive(false);
                break;

            case ItemStatus.Available:
                _status = ItemStatus.Available;
                gameObject.SetActive(true);
                break;

            case ItemStatus.PickedUp:
                _status = ItemStatus.PickedUp;
                gameObject.SetActive(false);
                break;

            case ItemStatus.Used:
                _status = ItemStatus.Used;
                PlayerController.Instance.UpdatePlayerData(_hp, _xp);
                Invoke("ReuseThisItem", 10f);
                break;

            default:
                Debug.LogError("Invalid item status: " + newStatus);
                break;
        }
    }

    void ReuseThisItem()
    {
        gameObject.SetActive(true);
        _status = ItemStatus.Available;
    }
}
