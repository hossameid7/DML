using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData Data;

    #region Variables
    public Rigidbody2D rb { get; private set; }
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public float LastOnGroundTime { get; private set; }

    private bool _isJumpCut;
    private bool _isJumpFalling;

    private Vector2 _moveInput;
    public float LastPressedJumpTime { get; private set; }

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.03f);
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = Data.gravityScale;
        IsFacingRight = true;
    }

    private void Update()
    {
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;

        #region INPUT HANDLER
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUpInput();
        }
        #endregion

        #region COLLISION CHECKS
        if (!IsJumping)
        {
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping) 
            {
                LastOnGroundTime = Data.coyoteTime; 
            }
        }
        #endregion

        #region JUMP CHECKS
        if (IsJumping && rb.velocity.y < 0)
        {
            IsJumping = false;
        }

        if (LastOnGroundTime > 0 && !IsJumping)
        {
            _isJumpCut = false;
        }

        //Jump
        if (LastOnGroundTime > 0 && !IsJumping && LastPressedJumpTime > 0)
        {
            IsJumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            Jump();
        }
        #endregion

        #region GRAVITY
        //Увеличиваем гравитацию после отпускания прыжка или во время падения.
        if (rb.velocity.y < 0 && _moveInput.y < 0)
        {
            //При нажатии вниз во время падения увеличиваем гравитацию ещё больше.
            rb.gravityScale = Data.gravityScale * Data.fastFallGravityMult;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -Data.maxFastFallSpeed));
        }
        else if (_isJumpCut)
        {
            //Если отпустить кнопку прыжка раньше, чем будет достигнута максимальная высота, увеличиваем гравитацию, чтобы получить эффект короткого прыжка.
            rb.gravityScale = Data.gravityScale * Data.jumpCutGravityMult;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -Data.maxFallSpeed));
        }
        else if ((IsJumping || _isJumpFalling) && Mathf.Abs(rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            rb.gravityScale = Data.gravityScale * Data.jumpHangGravityMult;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = Data.gravityScale * Data.fallGravityMult;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -Data.maxFallSpeed));
        }
        else
        {
            rb.gravityScale = Data.gravityScale;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        Run(1);
    }

    #region INPUT CALLBACKS
    public void OnJumpInput()
    {
        LastPressedJumpTime = Data.jumpInputBufferTime;
    }

    public void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;
    }
    #endregion

    #region RUN METHODS
    private void Run(float lerpAmount)
    {
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;
        //сглаживание скорости
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);

        //Расчёт ускорения, в воздухе умножаем на соответсвующие множители
        float accelRate;
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;

        //Увеличение ускорения и скорости на пике прыжка.
        if ((IsJumping || _isJumpFalling) && Mathf.Abs(rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }

        //Если игрок обрёл скорость, большую чем максимальная не замедляем его.
        if (Data.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
        {
            accelRate = 0;
        }

        float speedDif = targetSpeed - rb.velocity.x;
        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }
    #endregion

    private void Jump()
    {
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        float force = Data.jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private bool CanJumpCut()
    {
        return IsJumping && rb.velocity.y > 0;
    }
    #endregion
}