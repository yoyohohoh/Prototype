// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com | +1 (647) 637-0831
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiObjs;

    private Dictionary<string, GameObject> uiLookup;

    void Awake()
    {
        uiLookup = new Dictionary<string, GameObject>();
        foreach (GameObject uiObj in uiObjs)
        {
            uiLookup[uiObj.name] = uiObj;
            if(uiObj != uiObjs[0])
            {
                uiObj.SetActive(false);
            }
        }
            
    }

    public void ShowUI(string uiName)
    {
        if (uiLookup.TryGetValue(uiName, out GameObject uiObj))
        {
            uiObj.SetActive(true);
            uiObjs[0].SetActive(false);
        }
        else
        {
            Debug.LogWarning($"UI panel '{uiName}' not found.");
        }
    }

    public void HideUI(string uiName)
    {
        if (uiLookup.TryGetValue(uiName, out GameObject uiObj))
        {
            uiObj.SetActive(false);
            uiObjs[0].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"UI panel '{uiName}' not found.");
        }
    }
}
