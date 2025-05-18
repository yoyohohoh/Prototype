using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : PersistentSingleton<HUDManager>, IObserver
{
    [SerializeField] private GameObject playerDataPanel;
    TextMeshProUGUI nameTxt;
    TextMeshProUGUI levelTxt;
    Slider hpBar;
    Slider xpBar;

    
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

        UpdateHUD();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateHUD()
    {
        nameTxt = playerDataPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        levelTxt = playerDataPanel.transform.Find("Level").GetComponent<TextMeshProUGUI>();
        hpBar = playerDataPanel.transform.Find("HP").GetComponent<Slider>();
        xpBar = playerDataPanel.transform.Find("XP").GetComponent<Slider>();

        nameTxt.text = playerName;
        levelTxt.text = $"lv. {level}";
        hpBar.value = hp;
        hpBar.maxValue = 100f;
        xpBar.maxValue = maxXp;
        xpBar.value = xp;

    }
}
