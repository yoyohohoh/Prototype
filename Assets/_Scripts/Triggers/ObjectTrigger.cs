// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class ObjectTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> destinatedObjs;

    IEnumerator WakeUp(GameObject obj, float timeGap)
    {
        if (obj == null)
        {
            Debug.LogError("NPCTrigger: One of the destinated NPCs is null.");
        }

        NPCController controller = obj.GetComponent<NPCController>();
        if (controller == null)
        {
            Debug.LogError("NPCTrigger: The GameObject does not have an NPCController component.");
        }
        NavMeshAgent agent = controller.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NPCTrigger: The NPCController does not have a NavMeshAgent component.");
        }
        Animator animator = controller.animator;
        if (animator == null)
        {
            Debug.LogError("NPCTrigger: The NPCController does not have an Animator component.");
        }

        Debug.Log($"NPCTrigger: Activating {obj.name} after {timeGap} seconds.");

        yield return new WaitForSeconds(timeGap);

        obj.SetActive(true);
        if (controller)
        { controller.enabled = true; }
        if (agent)
        { agent.enabled = true; }
        if (animator)
        { animator.enabled = true; }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in destinatedObjs)
            {
                float randomTime = Random.Range(1f, 9f);
                StartCoroutine(WakeUp(obj, randomTime));
            }

        }
    }

}
