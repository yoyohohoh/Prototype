using UnityEngine;

public class LevelManager : PersistentSingleton<LevelManager>
{
    [SerializeField] public LevelConfig _levelConfig;
    public void GameSave()
    {
        GameSaveManager.Instance().SavePlayerData(PlayerController.Instance._playerData);
    }

    public void NewGame()
    {
        GameSaveManager.Instance().DeleteSaveData();
    }
}
