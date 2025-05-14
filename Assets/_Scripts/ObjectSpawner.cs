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

    [SerializeField] private bool isNPC, isItem;
    [SerializeField] private List<ObjCount> _objList;

    void Awake()
    {
        LevelConfig levelConfig = GameObject.Find("LevelManager").GetComponent<LevelManager>()._levelConfig;
        if(isItem)
        { _objList = levelConfig._itemList; }
        else if (isNPC)
        { _objList = levelConfig._npcList; }
        else
        { Debug.LogError("ObjectSpawner: No object type selected."); }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ObjCount itemCount in _objList)
        {
            for (int i = 0; i < itemCount.amount; i++)
            {
                Transform center = _spawnField.transform;

                int width = (int)((Mathf.Abs(center.localScale.x) * 10)/3) * 3;
                int depth = (int)((Mathf.Abs(center.localScale.z) * 10) / 3) * 3;
                float randomX = Random.Range(-width / 2f, width / 2f);
                float randomZ = Random.Range(-depth / 2f, depth / 2f);

                Vector3 spawnPosition = new Vector3(randomX, 1.0f, randomZ);
                Instantiate(itemCount.prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
