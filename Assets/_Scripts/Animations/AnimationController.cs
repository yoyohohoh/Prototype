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

[System.Serializable]
public class AnimationModifier
{
    public string animationParameter;
    public float delay = 0.0f;
}
public class AnimationController : MonoBehaviour
{
    [Header("Data")]
    private Vector3 flatVelocity = Vector3.zero;
    [SerializeField] private float sqrMagnitude = 0.0f;

    [Header("Animation")]
    [SerializeField] public Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private List <AnimationModifier> animationModifiers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();

        if (!animator || !characterController)
        {
            Debug.LogError("Animator or CharacterController is not assigned in AnimationController.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        flatVelocity = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);
        sqrMagnitude = flatVelocity.sqrMagnitude;

        animator.SetBool("isGrounded", characterController.isGrounded);
        animator.SetFloat("speed", sqrMagnitude);
        if (PlayerController.Instance.GetCurrentHealth() <= 0)
        {
            SetAnimationTrigger("Dead");
            this.enabled = false;
        }

    }

    public void SetArmed(bool isArmed)
    {
        animator.SetBool("isArmed", isArmed);
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
