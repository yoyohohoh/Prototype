// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : PersistentSingleton<QuestManager>, IObserver
{
    [SerializeField] QuestSet[] _questSets;
    [SerializeField] List<QuestSet> currentQuestSetList = new List<QuestSet>();
    private int playerLevel;
    void OnEnable()
    {
        PlayerController.Instance.AddObserver(this);
    }
    void OnDisable()
    {
        PlayerController.Instance.RemoveObserver(this);
    }

    public void OnNotify(PlayerData playerData)
    {
        playerLevel = playerData.level;
        UpdateQuest(playerLevel);
    }

    void UpdateQuest(int playerLevel)
    {
        Debug.Log($"Player Level: {playerLevel}");
        for (int i = 0; i < _questSets.Length; i++)
        {
            var questSet = _questSets[i];

            if (questSet._questLevel <= playerLevel &&
                questSet._questSetStatus != QuestSetStatus.Available &&
                !currentQuestSetList.Contains(questSet))
            {
                questSet._questSetStatus = QuestSetStatus.Available;
                currentQuestSetList.Add(questSet);
            }
        }
    }
}
