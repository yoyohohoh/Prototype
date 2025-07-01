// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ObjectTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> destinatedObjs;

    void WakeUp()
    {
        foreach (GameObject obj in destinatedObjs)
        {
            if (obj == null)
            {
                Debug.LogError("NPCTrigger: One of the destinated NPCs is null.");
                continue;
            }

            NPCController controller = obj.GetComponent<NPCController>();
            if (controller == null)
            {
                Debug.LogError("NPCTrigger: The GameObject does not have an NPCController component.");
                continue;
            }
            NavMeshAgent agent = controller.GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogError("NPCTrigger: The NPCController does not have a NavMeshAgent component.");
                continue;
            }
            Animator animator = controller.animator;
            if (animator == null)
            {
                Debug.LogError("NPCTrigger: The NPCController does not have an Animator component.");
                continue;
            }

            obj.SetActive(true);

            if (controller)
            { controller.enabled = true; }
            if (agent)
            { agent.enabled = true; }
            if (animator)
            { animator.enabled = true; }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("WakeUp", 1.0f);
        }
    }

}
