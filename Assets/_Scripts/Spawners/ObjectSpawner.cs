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
public enum ObjectType
{
    NPC,
    Collectible,
    Consumable,
    etc
}
public abstract class ObjectSpawner : MonoBehaviour
{
    protected LevelManager levelManager => LevelManager.Instance;
    protected LevelConfig levelConfig => levelManager?._levelConfig;
    [SerializeField] ObjectType objType;
    [SerializeField] List<ObjCount> _objList;
    [SerializeField] GameObject _spawnField;
    [SerializeField] float fieldOffset = 1;
    [SerializeField] float spawnYOffset = 0;

    protected virtual void Awake()
    {
        if (levelManager)
        {
            if (levelConfig)
            {
                switch (objType)
                {
                    case ObjectType.NPC:
                        _objList = levelConfig._npcList;
                        break;
                    case ObjectType.Collectible:
                        _objList = levelConfig._collectibleList;
                        break;
                    case ObjectType.Consumable:
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (_spawnField == null)
        {
            Debug.LogError("ObjectSpawner: _spawnField is not assigned.");
            return;
        }
        if (_objList == null || _objList.Count == 0)
        {
            Debug.LogWarning("ObjectSpawner: _objList is empty or null.");
            return;
        }

        foreach (ObjCount itemCount in _objList)
        {
            if (itemCount.amount <= 0)
            {
                itemCount.amount = Random.Range(1, 10);
            }
            for (int i = 0; i < itemCount.amount; i++)
            {
                SpawnObject(itemCount);
            }
        }
    }

    protected virtual void SpawnObject(ObjCount itemCount)
    {
        Transform center = _spawnField.transform;

        int width = (int)(Mathf.Abs(center.localScale.x * fieldOffset) / 3) * 3;
        int depth = (int)(Mathf.Abs(center.localScale.z * fieldOffset) / 3) * 3;
        float randomX = Random.Range(-width / 2f, width / 2f);
        float randomZ = Random.Range(-depth / 2f, depth / 2f);

        Vector3 spawnPosition = new Vector3(randomX, -center.position.y + spawnYOffset, randomZ) + center.position;
        GameObject itemObj = Instantiate(itemCount.prefab, spawnPosition, Quaternion.identity);
        itemObj.name = itemCount.prefab.name;

    }
}
