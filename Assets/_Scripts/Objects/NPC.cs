// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------
using UnityEngine;

public enum NPCStatus
{
    Idle,
    Patrol,
    Attack,
    Dead
}

public class NPC : MonoBehaviour
{
    public NPCStatus _npcStatus;

    public int _npcID;
    public string _npcName;
    public string _npcDescription;

    public int _npcLevel;
    public float _npcHp;

    public float _npcDamage => Mathf.Clamp(_npcLevel, 1, 99) * (Mathf.Clamp(_npcHp, 0, 100) / 100f);
}

