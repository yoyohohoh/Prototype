// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            if (characterController.velocity.magnitude < 0.1f)
            {
                animator.SetBool("isIdling", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
            }
            else
            {
                animator.SetBool("isIdling", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isJumping", false);
            }
        }
        else
        {
            animator.SetBool("isIdling", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", true);
        }



        Debug.Log("Magnitude: " + characterController.velocity.magnitude);
        Debug.Log("Velocity: " + characterController.velocity);

    }
}
