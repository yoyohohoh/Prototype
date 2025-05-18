// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : PersistentSingleton<HUDManager>, IObserver
{
    [Header("HUD")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private TextMeshProUGUI hpTxt;
    [SerializeField] private TextMeshProUGUI xpTxt;

    [Header("Player Data")]
    [SerializeField] private GameObject playerDataPanel;
    
    private string playerName;
    private int level;
    private float hp;
    private float xp;
    private float maxXp;

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
        playerName = playerData.name;
        level = playerData.level;
        hp = playerData.hp;
        xp = playerData.xp;
        maxXp = playerData.maxXp;

        UpdateHUD(hudPanel);
        UpdateHUDText(hpTxt, ((int)hp).ToString(), "100");
        UpdateHUDText(xpTxt, ((int)xp).ToString(), ((int)maxXp).ToString());
        UpdateHUD(playerDataPanel);
    }

    public void UpdateHUDText(TextMeshProUGUI text, string currentValue, string maxValue)
    {
        text.text = $"{currentValue}/{maxValue}";
    }

    public void UpdateHUD(GameObject panel)
    {
        TextMeshProUGUI nameTxt = panel.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI levelTxt = panel.transform.Find("Level").GetComponent<TextMeshProUGUI>();
        Slider hpBar = panel.transform.Find("HP").GetComponent<Slider>();
        Slider xpBar = panel.transform.Find("XP").GetComponent<Slider>();

        nameTxt.text = playerName;
        levelTxt.text = $"lv. {level}";
        hpBar.value = hp;
        hpBar.maxValue = 100f;
        xpBar.maxValue = maxXp;
        xpBar.value = xp;
    }
}
