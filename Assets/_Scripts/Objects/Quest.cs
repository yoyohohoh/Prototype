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
    OnProgress,
    Done
}

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public QuestStatus _questStatus = QuestStatus.OnProgress;

    public int _questID;
    public string _questName;
    public string _questDescription;

    [Header("Requirements")]
    public GameObject _checkPoint;
    public int _checkPointCount;
    public GameObject _questCollectable;
    public int _questCollectCount;
    public GameObject _questEnemy;
    public int _questKillCount;

    [Header("Reward")]
    public float _hpReward;
    public float _xpReward;
    public GameObject _questReward;

}


