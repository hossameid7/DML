using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    InputManager inputManager;
    PauseMenu pauseMenuObject;

    Vector3 moveDirection;
    Rigidbody playerRigidbody;
    BoxCollider playerCollider;

    public bool isGrounded;
    public bool isJumping;
    public bool lookingRight;

    public float fallingVelocity;
    public LayerMask groundLayer;
    public LayerMask jumpPadLayer;
    private float rayCastHeightOffset = 0.25f;

    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    public float moveSpeed = 15f;
    public float ledgeHeight = 0.2f;
    public float cayoteTiming = 0.125f;

    public float jumpPadStrength = 10f;
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();

        pauseMenuObject = FindObjectOfType<PauseMenu>();
        Debug.Log(pauseMenuObject.name);
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        HandleJumpCutting();
        HandleMovement();
        HandleRotation();
        //HandleJumpPad();
    }
    private void HandleMovement()
    {
        moveDirection = playerRigidbody.velocity;
        moveDirection.z = -inputManager.horizontalInput * moveSpeed;

        playerRigidbody.velocity = moveDirection;
    }

    private void HandleRotation()
    {
        if (inputManager.horizontalInput == 0)
            return;

        Vector3 targetDirection = Vector3.forward;
        targetDirection.z *= -inputManager.horizontalInput;
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
                            out _hit,
                            transform.rotation,
                            0.5f,
                            groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            isGrounded = true;
            if (_hit.collider.gameObject.tag == "Finish")
                pauseMenuObject.SetWinOrGameOver(true);
        }
        else
            isGrounded = false;
    }

    private void HandleJumpPad()
    {
        Vector3 boxCastOrigin = transform.position;
        boxCastOrigin.y += rayCastHeightOffset;
        if (Physics.BoxCast(boxCastOrigin, 
                            new Vector3(playerCollider.size.x / 2, 0.125f, playerCollider.size.z / 2),
                            -Vector3.up,
                            transform.rotation,
                            0.5f,
                            jumpPadLayer))
        {
            playerRigidbody.velocity.Set(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
            playerRigidbody.AddForce(new Vector3(0, Mathf.Sqrt(-2 * gravityIntensity * jumpPadStrength), 0), ForceMode.Impulse);
        }
    }
}
