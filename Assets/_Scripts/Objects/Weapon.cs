// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------
using UnityEngine;

public enum WeaponStatus
{
    NotAvailable,
    Available,
    PickedUp,
    Equipped
}

public class Weapon : MonoBehaviour
{
    public WeaponStatus _status;

    public int _id;
    public string _name;
    public string _description;
    public Sprite _icon;

    public float _damage;

    public void SetStatus(WeaponStatus newStatus)
    {
        _status = newStatus;

        switch (_status)
        {
            case WeaponStatus.NotAvailable:
                gameObject.SetActive(false);
                break;

            case WeaponStatus.Available:
                gameObject.SetActive(true);
                break;

            case WeaponStatus.PickedUp:
                gameObject.SetActive(false);
                break;

            case WeaponStatus.Equipped:
                gameObject.SetActive(true);
                PlayerController.Instance.PutWeapon(gameObject);
                break;

            default:
                Debug.LogError("Invalid weapon status: " + _status);
                break;
        }
    }
}
