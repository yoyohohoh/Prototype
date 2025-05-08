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
