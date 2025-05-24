// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
public enum QuestSetStatus
{
    NotAvailable,
    Available,
    Completed
}

[CreateAssetMenu(fileName = "QuestSet", menuName = "Scriptable Objects/QuestSet")]
public class QuestSet : ScriptableObject
{
    public QuestSetStatus _questSetStatus = QuestSetStatus.NotAvailable;
    public int _questLevel;
    public string _questSetName;
    public Quest[] _quests;
}