using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    GrapplingGun grapplingGun;
    public UnityEngine.UI.Image adviceBox;
    public Text movementAdvice, jumpingAdvice, grapplingAdvice;

    public bool isInteracting;
    public bool firstTimeMoved = false;
    public bool firstTimeJumped = false;
    public bool passedGrappleTrigger = false;
    public bool firstTimeGrappled = false;

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

    public void HandleAdvices()
    {
        if (firstTimeMoved) Destroy(movementAdvice);

        if (firstTimeJumped) Destroy(jumpingAdvice);

        if (firstTimeJumped && firstTimeMoved && !passedGrappleTrigger) adviceBox.enabled = false;

        if (passedGrappleTrigger)
        {
            adviceBox.enabled = true;
            grapplingAdvice.enabled = true;
        }

        if (passedGrappleTrigger && firstTimeGrappled) 
        {
            Destroy(grapplingAdvice);
            Destroy(adviceBox);
        }
    }
}
