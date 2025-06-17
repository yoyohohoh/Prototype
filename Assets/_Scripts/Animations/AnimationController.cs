// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
public enum identityType
{
    Player,
    NPC,
}

[System.Serializable]
public class AnimationModifier
{
    public string animationParameter;
    public float delay = 0.0f;
}
public class AnimationController : MonoBehaviour
{
    [Header("Identity")]
    [SerializeField] private identityType identity;

    [Header("Data")]
    private Vector3 flatVelocity = Vector3.zero;
    [SerializeField] private float sqrMagnitude = 0.0f;

    [Header("Animation")]
    [SerializeField] public Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private List <AnimationModifier> animationModifiers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (identity)
        {
            case identityType.Player:
                characterController = GetComponent<CharacterController>();
                navMeshAgent = null;
                break;
            case identityType.NPC:
                navMeshAgent = GetComponent<NavMeshAgent>();
                characterController = null;
                break;
            default:
                Debug.LogError("Identity type not set correctly. Please choose either Player or NPC.");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

        flatVelocity = GetFlatVelocity();
        sqrMagnitude = flatVelocity.sqrMagnitude;

        animator.SetBool("isGrounded", characterController.isGrounded);
        animator.SetFloat("speed", sqrMagnitude);
        if (GetHP() <= 0)
        {
            SetAnimationTrigger("Dead");
            this.enabled = false;
        }

    }

    public void SetArmed(bool isArmed)
    {
        animator.SetBool("isArmed", isArmed);
    }

    float GetHP()
    {
        if (identity == identityType.Player)
        {
            return this.GetComponent<PlayerController>().GetCurrentHealth();
        }
        else if (identity == identityType.NPC)
        {
            return this.GetComponent<NPC>().GetHP();
        }
        return 0f;
    }

    Vector3 GetFlatVelocity()
    {
        if (identity == identityType.Player && characterController != null)
        {
            return new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);
        }
        else if (identity == identityType.NPC && navMeshAgent != null)
        {
            return new Vector3(navMeshAgent.velocity.x, 0f, navMeshAgent.velocity.z);
        }
        return Vector3.zero;
    }

    public void SetAnimationTrigger(string triggerName)
    {
        if (animator != null && !string.IsNullOrEmpty(triggerName))
        {
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("Animator or trigger name is not set correctly.");
        }
    }

    public IEnumerator PlayCollectAnimation(string triggerName, GameObject other, Transform hand)
    {
        AnimationModifier modifier = animationModifiers.Find(mod => mod.animationParameter == triggerName);
        float delay = 0.0f;
        if (modifier != null)
        {
            delay = modifier.delay;
        }
        else
        {
            Debug.LogWarning("Animation trigger not found: " + triggerName);
        }

        this.GetComponent<CharacterController>().enabled = false;
        SetAnimationTrigger(triggerName);
        yield return new WaitForSeconds(delay);
        other.transform.SetParent(hand);
        other.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - delay);

        this.GetComponent<CharacterController>().enabled = true;
    }
}
