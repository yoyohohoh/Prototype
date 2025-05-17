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
}

public class Weapon : MonoBehaviour
{
    public WeaponStatus _weaponStatus;

    public int _id;
    public string _name;
    public string _description;
    public Sprite _icon;

    public float _damage;
}
