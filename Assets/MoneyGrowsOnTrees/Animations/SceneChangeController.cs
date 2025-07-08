// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SceneLoad(int sceneID)
    {
        StartCoroutine(SceneLoadDelay(sceneID));
    }

    IEnumerator SceneLoadDelay(int sceneID)
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneID);
    }
}
