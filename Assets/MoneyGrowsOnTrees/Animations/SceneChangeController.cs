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
    [SerializeField] Animator animator;
    void Start()
    {
        
    }

    public void SceneLoad(int sceneID)
    {
        StartCoroutine(SceneLoadDelay(sceneID));
    }

    IEnumerator SceneLoadDelay(int sceneID)
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneID);
    }
}
