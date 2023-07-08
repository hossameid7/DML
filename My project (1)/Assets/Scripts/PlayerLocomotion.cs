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
    public bool lookingRight;

    public float fallingVelocity;
    public LayerMask groundLayer;
    public LayerMask jumpPadLayer;
    private float rayCastHeightOffset = 0.25f;

    public bool flipX = true;
    public bool flipY;
    public bool flipZ;

    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    public float moveSpeed = 15f;
    public float cayoteTiming = 0.125f;

    public float jumpPadStrength = 10f;
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
        //HandleJumpPad();
    }
    private void HandleMovement()
    {
        moveDirection = playerRigidbody.velocity;
        moveDirection.z = -inputManager.horizontalInput * moveSpeed;

        playerRigidbody.velocity = moveDirection;
        //playerRigidbody.AddForce(-Vector3.forward * inputManager.horizontalInput * moveSpeed);
    }

    private void HandleRotation()
    {
        if (inputManager.horizontalInput == 0)
            return;

        //if (lookingRight && inputManager.horizontalInput == -1)
        //    return;
        //if (!lookingRight && inputManager.horizontalInput == 1)
        //    return;

        //Vector3 targetDirection = transform.position;
        //var t = transform.rotation.eulerAngles;
        //var newRotation = Quaternion.Euler(-t.x, 0, 0);
        //transform.rotation = newRotation;

        //targetDirection.x = 0;
        //targetDirection = targetDirection.normalized;

        Vector3 targetDirection = Vector3.forward;
        targetDirection.z *= -inputManager.horizontalInput;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = targetRotation;
    }

    //void Flip()
    //{
    //    if (mesh == null) return;
    //    Vector3[] verts = mesh.vertices;
    //    for (int i = 0; i < verts.Length; i++)
    //    {
    //        Vector3 c = verts[i];
    //        if (flipX) c.x *= -1;
    //        if (flipY) c.y *= -1;
    //        if (flipZ) c.z *= -1;
    //        verts[i] = c;
    //    }

    //    mesh.vertices = verts;
    //    if (flipX ^ flipY ^ flipZ) FlipNormals();
    //}

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
