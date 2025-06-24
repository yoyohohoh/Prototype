// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class  WeaponMaterial
{
    public string name;
    public Color color;
    public Material material;
}
public class WeaponMaterialModifier : MonoBehaviour
{
    [SerializeField] List<WeaponMaterial> weaponMaterials;
    [SerializeField] string currentOption;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMaterial();
    }

    Material GetMaterialByName(string name)
    {
        foreach (var wm in weaponMaterials)
        {
            if (wm.name == name)
                return wm.material;
        }
        return null;
    }

    public Color GetColorByName()
    {
        foreach (var wm in weaponMaterials)
        {
            if (wm.name == currentOption)
                return wm.color;
        }
        return new Color(1, 1, 1, 1);
    }

    public void SetMaterial()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = GetMaterialByName(currentOption);
            if (mat != null)
            {
                renderer.material = mat;
            }
            else
            {
                Debug.LogWarning($"Material with name '{currentOption}' not found.");
            }
        }
    }
}
