// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
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
