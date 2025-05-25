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
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuestManager : PersistentSingleton<QuestManager>, IObserver
{
    [Header("Panels")]
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject questsContentPanel;
    [SerializeField] GameObject questsPrefab;
    [SerializeField] TextMeshProUGUI questsCountText;

    [Header("Scroll Bar")]
    [SerializeField] Scrollbar questsScrollbar;
    [SerializeField] float questsScrollbarMinY = 0f;
    [SerializeField] float questsScrollbarMaxY = -300f;
    [SerializeField] float questsScrollbarPosY = 0f;
    [SerializeField] float questsScrollbarSpacing = 300f;

    [Header("Quest Details")]
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
    void Start()
    {
        
    }
    void Update()
    {
        // tracking if player finish quest
        // how many checkPoint collected
        // how many inventory stored
        // how many npc "dead"

    }
    void UpdateCount()
    {
        List<QuestStatus> questsList = new List<QuestStatus>();

        foreach (QuestSet questSet in currentQuestSetList)
        {
            foreach (Quest quest in questSet._quests)
            {
                questsList.Add((quest._questStatus));
                questsContentPanel.transform.Find(quest._questName).Find("QuestStatus").GetComponent<Image>().sprite = quest._questStatus == QuestStatus.OnProgress ? Resources.Load<Sprite>("Sprites/blankBadge") : Resources.Load<Sprite>("Sprites/badge");
            }
        }

        int progressCount = questsList.Count(status => status == QuestStatus.OnProgress);
        int totalCount = questsList.Count;

        questsCountText.text = $"{progressCount}/{totalCount}";
    }

    void UpdatePanel(QuestSet currentQuestSet)
    {
        foreach (Quest quest in currentQuestSet._quests)
        {
            GameObject newQuest = Instantiate(questsPrefab, questsContentPanel.transform);
            newQuest.name = quest._questName;
            newQuest.transform.Find("QuestName").GetComponent<TextMeshProUGUI>().text = $"{currentQuestSet._questSetName}: {quest._questName}";
            newQuest.transform.Find("QuestDescription").GetComponent<TextMeshProUGUI>().text = quest._questDescription;
            newQuest.transform.Find("QuestStatus").GetComponent<Image>().sprite = quest._questStatus == QuestStatus.OnProgress ? Resources.Load<Sprite>("Sprites/blankBadge") : Resources.Load<Sprite>("Sprites/badge");
            RectTransform questRT = newQuest.GetComponent<RectTransform>();
            questRT.localScale = Vector3.one;
            questRT.anchoredPosition = new Vector2(0f, questsScrollbarPosY);
            questsScrollbarMaxY += questsScrollbarSpacing;
            questsScrollbarPosY -= questsScrollbarSpacing;
        }
    }

    public void ScrollPanel()
    {
        RectTransform rt = questsContentPanel.GetComponent<RectTransform>();
        Vector2 pos = rt.anchoredPosition;
        pos.y = Mathf.Lerp(questsScrollbarMinY, questsScrollbarMaxY, questsScrollbar.value);
        rt.anchoredPosition = pos;
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
                UpdatePanel(questSet);
            }
        }
        UpdateCount();

    }
}
