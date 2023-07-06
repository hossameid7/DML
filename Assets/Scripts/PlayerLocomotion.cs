using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    InputManager inputManager; 

    Vector3 moveDirection;
    Rigidbody playerRigidbody;
    BoxCollider playerCollider;

    public bool isGrounded;
    public bool isJumping;

    public float fallingVelocity;
    public LayerMask groundLayer;
    private float rayCastHeightOffset = 0.25f;

    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    public float moveSpeed = 15f;
    public float cayoteTiming;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        HandleJumpCutting();
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        moveDirection = playerRigidbody.velocity;
        moveDirection.x = inputManager.horizontalInput * moveSpeed;

        playerRigidbody.velocity = moveDirection;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.right;
        if (inputManager.horizontalInput == 0)
            return;

        targetDirection *= inputManager.horizontalInput;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = targetRotation;
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            playerRigidbody.AddForce(new Vector3(0, jumpingVelocity, 0), ForceMode.Impulse); 
        }
    }

    private void HandleJumpCutting()
    {
        if (CanJumpCut() && inputManager.jumpCut)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
            inputManager.jumpCut = false;
        }
    }

    private bool CanJumpCut()
    {
        return !isGrounded && isJumping && playerRigidbody.velocity.y > 0;
    }
    private void HandleFallingAndLanding()
    {
        RaycastHit _hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += rayCastHeightOffset;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            playerRigidbody.AddForce(-Vector3.up * fallingVelocity);
        }

        float cayoteHalfExtent = Mathf.Abs(playerRigidbody.velocity.x) * cayoteTiming / 2;
        if (Physics.BoxCast(rayCastOrigin,
                            new Vector3(playerCollider.size.x / 2, 0.125f, playerCollider.size.z / 2 + cayoteHalfExtent),
                            -Vector3.up,
                            transform.rotation,
                            0.5f,
                            groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            isGrounded = true;
        }
        else
            isGrounded = false;
    }
}
