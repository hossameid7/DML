using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [HideInInspector] public float gravityStrength; 
    [HideInInspector] public float gravityScale;

    public float fallGravityMult; //���������, ��� ���������� ���������� �� ����� �������.
    public float maxFallSpeed; //�����������, ����� �� ����� ������� �� ����������� �������.

    public float fastFallGravityMult; //��������� ��� �������� �������.
    public float maxFastFallSpeed; //��������������� �����������.


    public float runMaxSpeed; //�������� ��������.
    public float runAcceleration; //���������, ��� ������ ��������(==runMaxSpeed => �������� ����� � ������������ ���������)
    [HideInInspector] public float runAccelAmount; 
    public float runDecceleration;
    [HideInInspector] public float runDeccelAmount; 
    [Range(0f, 1)] public float accelInAir; //��������� ��������� � �������.
    [Range(0f, 1)] public float deccelInAir;
    public bool doConserveMomentum = true;


    public float jumpHeight; //������������ ������ ������
    public float jumpTimeToApex; //�����, �� ������� �� ����� ���������� ������������ ������ ������.
    [HideInInspector] public float jumpForce; 

    public float jumpCutGravityMult; 
    [Range(0f, 1)] public float jumpHangGravityMult; 
    public float jumpHangTimeThreshold; //Speeds (close to 0) where the player will experience extra "jump hang". The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)

    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Range(0.01f, 0.5f)] public float coyoteTime; 
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime; 


    //������������� ��������, ��� ��������� ���������� 
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