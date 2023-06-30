using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [HideInInspector] public float gravityStrength; 
    [HideInInspector] public float gravityScale;

    public float fallGravityMult; //Множитель, для увеличения гравитации во время падения.
    public float maxFallSpeed; //Ограничение, чтобы во время падения не разогнаться слишком.

    public float fastFallGravityMult; //Множитель для быстрого падения.
    public float maxFastFallSpeed; //Соответствующее ограничение.


    public float runMaxSpeed; //Скорость движения.
    public float runAcceleration; //Ускорение, при начале движения(==runMaxSpeed => начинаем сразу с максимальной скоростью)
    [HideInInspector] public float runAccelAmount; 
    public float runDecceleration;
    [HideInInspector] public float runDeccelAmount; 
    [Range(0f, 1)] public float accelInAir; //Множители ускорения в воздухе.
    [Range(0f, 1)] public float deccelInAir;
    public bool doConserveMomentum = true;


    public float jumpHeight; //Максимальная высота прыжка
    public float jumpTimeToApex; //Время, за которое мы хотим достигнуть максимальной высоты прыжка.
    [HideInInspector] public float jumpForce; 

    public float jumpCutGravityMult; 
    [Range(0f, 1)] public float jumpHangGravityMult; 
    public float jumpHangTimeThreshold; //Speeds (close to 0) where the player will experience extra "jump hang". The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)

    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Range(0.01f, 0.5f)] public float coyoteTime; 
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime; 


    //Пересчитываем значения, при изменении параметров 
    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        gravityScale = gravityStrength / Physics2D.gravity.y;

        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
}