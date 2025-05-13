// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;

public enum QuestStatus
{
    NotAvailable,
    OnProgress,
    Done
}

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public QuestStatus _questStatus;

    public int _questID;
    public string _questName;
    public string _questDescription;

    public float _questHp;
    public float _questXp;
    public GameObject _questReward;
}


