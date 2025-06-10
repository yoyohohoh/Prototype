// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : ObjectSpawner
{
    [SerializeField] List<ObjCount> moneyList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        
    }

    // Update is called once per frame
    protected override void Start()
    {
        foreach (ObjCount itemCount in moneyList)
        {
            for (int i = 0; i < itemCount.amount; i++)
            {
                SpawnObject(itemCount);
            }
        }
    }
}
