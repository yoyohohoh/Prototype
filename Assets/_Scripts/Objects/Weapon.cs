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

    public int _weaponID;
    public string _weaponName;
    public string _weaponDescription;

    public float _weaponDamage;
}
