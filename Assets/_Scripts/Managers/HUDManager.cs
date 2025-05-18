using UnityEngine;

public class HUDManager : PersistentSingleton<HUDManager>, IObserver
{
    public int level;
    public float hp;
    public float xp;
    public float maxXp;

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
        level = playerData.level;
        hp = playerData.hp;
        xp = playerData.xp;
        maxXp = playerData.maxXp;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
