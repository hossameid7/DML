using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    GrapplingGun grapplingGun;

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        grapplingGun = GetComponentInChildren<GrapplingGun>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);

        grapplingGun.DrawGrapplingGunRope();
    }
}
