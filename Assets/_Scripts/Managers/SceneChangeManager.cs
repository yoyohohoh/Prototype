// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : PersistentSingleton<SceneChangeManager>
{
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameDataLoad();
        }
    }
    public void GameDataLoad()
    {
        PlayerData playerData = GameSaveManager.Instance().LoadPlayerData();
        if (playerData == null)
        {
            LoadSceneByName("CutScene1");
        }
        else
        {
            LoadSceneByName("Menu");
        }
    }
    public void LoadSceneByName(string sceneName)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
