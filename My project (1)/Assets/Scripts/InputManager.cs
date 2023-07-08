using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;
    GrapplingGun grapplingGun;

    public Vector2 movementInput;
    public float horizontalInput;

    public bool jumpInput;
    public bool jumpCut;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        grapplingGun = GetComponentInChildren<GrapplingGun>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += context => movementInput = context.ReadValue<Vector2>();

            playerControls.PlayerActions.Jump.started += context =>
            {
                if (context.interaction is TapInteraction)
                    jumpInput = true;
            };

            playerControls.PlayerActions.Jump.performed += context =>
            {
                if (context.interaction is TapInteraction)
                    jumpCut = true;
            };

            playerControls.PlayerActions.RMB.started += context =>
            {
                Vector2 mousePosition = playerControls.PlayerActions.MousePosition.ReadValue<Vector2>();
                grapplingGun.StartGrapple(mousePosition);
            };

            playerControls.PlayerActions.RMB.canceled += context =>
            {
                grapplingGun.StopGrapple();
            };

            playerControls.PlayerActions.RMB.canceled += context => grapplingGun.StopGrapple();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        animatorManager.UpdateAnimatorValues(Mathf.Abs(horizontalInput));
    }

    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }
}
