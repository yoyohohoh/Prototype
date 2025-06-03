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
using Unity.Android.Gradle.Manifest;
public enum QuestCategory
{
    checkpoint,
    npc,
    item
}
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
    private int playerLevel;
    [SerializeField] List<string> checkPointsCollected;
    [SerializeField] List<string> itemCollected;
    [SerializeField] List<string> npcCollected;

    [SerializeField] QuestSet[] _questSets;
    [SerializeField] List<QuestSet> currentQuestSetList = new List<QuestSet>();
    void OnEnable()
    {
        PlayerController.Instance.AddObserver(this);
    }
    void OnDisable()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.RemoveObserver(this);
        }
    }

    public void OnNotify(PlayerData playerData)
    {
        playerLevel = playerData.level;
        UpdateQuest();
    }
    void Start()
    {
        if (GameSaveManager.Instance().LoadPlayerData() != null)
        {
            PlayerData playerData = GameSaveManager.Instance().LoadPlayerData();
            playerLevel = playerData.level;
            checkPointsCollected = playerData.checkPoints;
            npcCollected = playerData.npcKilled;
            itemCollected = playerData.inventory
                .Where(slot => !slot.isEmpty)
                .Select(slot => slot.itemName)
                .ToList();

        }
        else
        {
            checkPointsCollected = new List<string>();
            npcCollected = new List<string>();
            itemCollected = new List<string>();
        }
    }
    void Update()
    {
        // tracking if player finish quest
        // how many checkPoint collected // own storage  list

        // how many inventory stored // items & weapons

        // how many objEntry "dead" // NPC State



    }
    public List<string> GetList(QuestCategory listName)
    {
        switch (listName)
        {
            case QuestCategory.checkpoint:
                return checkPointsCollected;
            case QuestCategory.npc:
                return npcCollected;
            case QuestCategory.item:
                return itemCollected;
            default:
                return null;
        }
    }
    void CheckItems()
    {

    }
    public void AddObjForQuest(QuestCategory category, GameObject obj)
    {
        switch (category)
        {
            case QuestCategory.item:
                itemCollected.Add(obj.name);
                CheckCollection(QuestCategory.item);
                break;
            case QuestCategory.npc:
                npcCollected.Add(obj.name);
                CheckCollection(QuestCategory.npc);
                break;
        }
        
    }

    void CheckCollection(QuestCategory category)
    {
        bool needUpdateQuest = false;
        foreach (QuestSet questSet in currentQuestSetList)
        {
            foreach (Quest quest in questSet._quests)
            {
                if (quest._questStatus == QuestStatus.OnProgress)
                {
                    List<ObjEntry> targetList = new List<ObjEntry>();
                    List<string> collectedList = new List<string>();

                    switch(category)
                    {
                        case QuestCategory.item:
                            targetList = quest._itemList;
                            collectedList = itemCollected;
                            break;
                        case QuestCategory.npc:
                            targetList = quest._npcKillList;
                            collectedList = npcCollected;
                            break;
                    }

                    if (targetList.Any())
                    {
                        bool isQuestDone = targetList.All(objEntry => collectedList.Count(n => n == objEntry.obj.name) >= objEntry.count);

                        if (isQuestDone)
                        {
                            quest._questStatus = QuestStatus.Done;

                            foreach (ObjEntry npc in targetList)
                            {
                                for (int i = 0; i < npc.count; i++)
                                {
                                    collectedList.Remove(npc.obj.name);
                                }
                            }

                            needUpdateQuest = true;
                        }
                    }
                }
            }
        }
        if (needUpdateQuest)
        {
            UpdateQuest();
        }
    }

    public void AddCheckPoint(GameObject checkpoint)
    {
        if (!checkPointsCollected.Contains(checkpoint.name))
        {
            checkPointsCollected.Add(checkpoint.name);
        }

        CheckCheckPoint();
    }

    void CheckCheckPoint()
    {
        bool needUpdateQuest = false;

        foreach (QuestSet questSet in currentQuestSetList)
        {
            foreach (Quest quest in questSet._quests)
            {
                if (quest._questStatus == QuestStatus.OnProgress)
                {
                    if (quest._checkPoints.Any())
                    {
                        if (quest._checkPoints.All(cp => checkPointsCollected.Contains(cp)))
                        {
                            quest._questStatus = QuestStatus.Done;
                            needUpdateQuest = true;
                        }
                    }
                }
            }
        }

        if (needUpdateQuest)
        {
            UpdateQuest();
        }
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

        int progressCount = questsList.Count(status => status == QuestStatus.Done);
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
    public void ResetScrollBar()
    {
        questsScrollbar.value = 0f;
    }
    public void UpdateQuest()
    {
        currentQuestSetList.Clear();

        foreach (Transform child in questsContentPanel.transform)
        {
            Destroy(child.gameObject);
            questsScrollbarMinY = 0f;
            questsScrollbarMaxY = -300f;
            questsScrollbarPosY = 0f;
            questsScrollbarSpacing = 300f;
        }



        for (int i = 0; i < _questSets.Length; i++)
        {
            var questSet = _questSets[i];

            if (questSet._questLevel <= playerLevel && questSet._questSetStatus != QuestSetStatus.Completed)
            {
                questSet._questSetStatus = QuestSetStatus.Available;

                if (!currentQuestSetList.Contains(questSet))
                {
                    currentQuestSetList.Add(questSet);
                    UpdatePanel(questSet);
                }
            }
        }
        foreach (QuestSet questSet in currentQuestSetList)
        {
            if (questSet._quests.All(q => q._questStatus == QuestStatus.Done))
            {
                questSet._questSetStatus = QuestSetStatus.Completed;
                LevelManager.Instance.GameSave();
            }
        }

        UpdateCount();

    }
}
