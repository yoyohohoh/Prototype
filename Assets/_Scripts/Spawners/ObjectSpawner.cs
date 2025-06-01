// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjCount
{
    public GameObject prefab;
    public int amount;
}

public abstract class ObjectSpawner : MonoBehaviour
{
    public string objType;
    public List<ObjCount> _objList;
    public GameObject _spawnField;

    protected virtual void Awake()
    {
        LevelConfig levelConfig = LevelManager.Instance._levelConfig;
        if (levelConfig != null)
        {
            switch (objType)
            {
                case "NPC":
                    _objList = levelConfig._npcList;
                    break;
                case "Collectible":
                    _objList = levelConfig._collectibleList;
                    break;
                case "Consumable":
                    _objList = levelConfig._consumableList;
                    break;
                default:
                    Debug.LogError("ObjectSpawner: Invalid objType specified.");
                    break;
            }

        }
        else
        {
            Debug.LogError("ObjectSpawner: LevelConfig is not set in LevelManager.");
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        foreach (ObjCount itemCount in _objList)
        {
            for (int i = 0; i < itemCount.amount; i++)
            {
                SpawnObject(itemCount);
            }
        }
    }

    protected virtual void SpawnObject(ObjCount itemCount)
    {
        Transform center = _spawnField.transform;

        int width = (int)(Mathf.Abs(center.localScale.x) / 3) * 3;
        int depth = (int)(Mathf.Abs(center.localScale.z) / 3) * 3;
        float randomX = Random.Range(-width / 2f, width / 2f);
        float randomZ = Random.Range(-depth / 2f, depth / 2f);

        Vector3 spawnPosition = new Vector3(randomX, 0.0f, randomZ);
        GameObject itemObj = Instantiate(itemCount.prefab, spawnPosition, Quaternion.identity);
        itemObj.name = itemCount.prefab.name;
    }
}
