// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : ObjectSpawner
{
    [SerializeField] private List<Transform> startpoints;
    [SerializeField] private List<Transform> endpoints;
    protected override void Awake()
    {
        base.Awake();

        startpoints = FindTransformListByTag("StartPoint");
        endpoints = FindTransformListByTag("EndPoint");
    }

    List<Transform> FindTransformListByTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        List<Transform> transforms = new List<Transform>();

        foreach (GameObject obj in objects)
        {
            transforms.Add(obj.transform);
        }

        return transforms;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void SpawnObject(ObjCount itemCount)
    {
        int randomStart = Random.Range(0, startpoints.Count);
        int randomEnd = Random.Range(0, endpoints.Count);
        GameObject npcObj = Instantiate(itemCount.prefab, startpoints[randomStart].position, Quaternion.identity);
        NPCController npcController = npcObj.GetComponent<NPCController>();
        if (npcController == null)
        {
            Debug.LogError("NPCSpawner: Spawned object does not have an NPC component.");
            return;
        }
        npcController.SetLocation(Location.Origin, startpoints[randomStart]);
        npcController.SetLocation(Location.Destination, endpoints[randomEnd]);
        npcObj.name = itemCount.prefab.name;
    }

}
