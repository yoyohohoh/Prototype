// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioConfig
{
    public AudioSource audioSource;
    public float gap;
}
public class AudioController : MonoBehaviour
{
    [SerializeField] float startDelay = 1.0f;
    [SerializeField] List<AudioConfig> audios;
    void Awake()
    {
        PlayerData playerData = GameSaveManager.Instance().LoadPlayerData();
        if (playerData == null)
        {
            foreach (AudioConfig config in audios)
            {
                if (config.audioSource == null)
                {
                    Debug.LogError("AudioController: One of the audios is not assigned.");
                }
            }

            StartCoroutine(AudioStart());
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private IEnumerator AudioStart()
    {
        yield return new WaitForSeconds(startDelay);
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].audioSource != null)
            {
                if (i > 0)
                { audios[i - 1].audioSource.enabled = false; }
                audios[i].audioSource.enabled = true;
            }

            yield return new WaitForSeconds(audios[i].gap);
        }
    }

}
