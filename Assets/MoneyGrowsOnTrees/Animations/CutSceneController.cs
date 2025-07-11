// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AnimationConfig
{
    public Animator animator;
    public float gap;
}
public class CutSceneController : MonoBehaviour
{
    [SerializeField] List<AnimationConfig> animators;
    void Awake()
    {
        foreach (AnimationConfig config in animators)
        {
            if (config.animator == null)
            {
                Debug.LogError("CutSceneController: One of the animators is not assigned.");
            }
        }

        StartCoroutine(CutSceneStart());
    }

    IEnumerator CutSceneStart()
    {
        Animator Fading = animators[0].animator;

        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].animator.enabled = true;
            yield return new WaitForSeconds(animators[i].gap);         
        }

        Fading.SetTrigger("Exit");
        yield return new WaitForSeconds(Fading.GetCurrentAnimatorStateInfo(0).length + 2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
