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

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnField;
    [SerializeField] private List<ObjCount> _objList;

    void Awake()
    {
        LevelConfig levelConfig = GameObject.Find("LevelManager").GetComponent<LevelManager>()._levelConfig;
        _objList = levelConfig._collectibleList;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ObjCount itemCount in _objList)
        {
            for (int i = 0; i < itemCount.amount; i++)
            {
                Transform center = _spawnField.transform;

                int width = (int)(Mathf.Abs(center.localScale.x) /3) * 3;
                int depth = (int)(Mathf.Abs(center.localScale.z) / 3) * 3;
                float randomX = Random.Range(-width / 2f, width / 2f);
                float randomZ = Random.Range(-depth / 2f, depth / 2f);

                Vector3 spawnPosition = new Vector3(randomX, 0.0f, randomZ);
                GameObject itemObj = Instantiate(itemCount.prefab, spawnPosition, Quaternion.identity);
                itemObj.name = itemCount.prefab.name;
            }
        }
    }
}
