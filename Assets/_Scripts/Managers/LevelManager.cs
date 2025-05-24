using UnityEngine;

public class LevelManager : PersistentSingleton<LevelManager>
{
    [SerializeField] public LevelConfig _levelConfig;
    public void GameSave()
    {
        PlayerController.Instance._playerData.items = InventoryManager.Instance.items;
        PlayerController.Instance._playerData.weapons = InventoryManager.Instance.weapons;
        Debug.Log($"LevelManager: {PlayerController.Instance._playerData.items[0].isEmpty}");
        GameSaveManager.Instance().SavePlayerData(PlayerController.Instance._playerData);
    }

    public void NewGame()
    {
        GameSaveManager.Instance().DeleteSaveData();
    }
}
