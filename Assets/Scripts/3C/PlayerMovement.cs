using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_MaxSpeed = 1.0f;
    [SerializeField]
    private float m_RunSpeedMultiplier = 2.0f;
    [SerializeField]
    private float m_JumpHeight = 1.0f;
    [SerializeField]
    private float m_JumpDeceleration = 0.1f;
    [SerializeField]
    private float m_SlideDeceleration = 0.1f;
    [SerializeField]
    private float m_GroundDeceleration = 0.5f;
    [SerializeField]
    private GaugeChannel m_SprintGaugeChannel;
    [SerializeField]
    private GaugeChannel m_SpeedGauge;
    [SerializeField]
    private float m_IdleSprintGaugeFillRate = 0.5f;
    [SerializeField]
    private float m_WalkSprintGaugeFillRate = 0.1f;
    [SerializeField]
    private float m_SprintGaugeDecelerationRate = 0.5f;

    [SerializeField]
    private AudioChannel m_AudioChannel;
    [SerializeField]
    private AudioClip[] m_PossibleJumpClips;
    private System.Random m_Random = new System.Random();



    private CharacterController m_CharacterController;
    private CrouchBehavior m_CrouchBehavior;
    private Vector3 m_Velocity = Vector3.zero;
    private float m_SprintGauge = 1.0f;
    private bool m_CanSprint = true;
    private readonly float m_GravityValue = -9.81f;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_CrouchBehavior = GetComponent<CrouchBehavior>();
    }

    private void FixedUpdate()
    {
        bool isGrounded = m_CharacterController.isGrounded;
        if (isGrounded && m_Velocity.y < 0)
        {
            ResetFallVelocity();
        }

        Vector3 move = Input.GetAxis("Horizontal") * transform.right +
                        Input.GetAxis("Vertical") * transform.forward;

        float speed = m_MaxSpeed * UpdateSprintModifier(move.sqrMagnitude > 0);
        m_CharacterController.Move(move * Time.fixedDeltaTime * speed);

        if (Input.GetButton("Jump") && isGrounded)
        {
            m_AudioChannel.RaisePlayAudioRequest(m_PossibleJumpClips[m_Random.Next(m_PossibleJumpClips.Length)]);
            AddJumpImpulse();
        }

        m_Velocity.y += m_GravityValue * Time.fixedDeltaTime;

        float deceleration = isGrounded ? m_CrouchBehavior.IsCrouching ? m_SlideDeceleration : m_GroundDeceleration : m_JumpDeceleration;
        m_Velocity.x = Mathf.Lerp(m_Velocity.x, 0.0f, deceleration * Time.fixedDeltaTime);
        m_Velocity.z = Mathf.Lerp(m_Velocity.z, 0.0f, deceleration * Time.fixedDeltaTime);

        m_CharacterController.Move(m_Velocity * Time.fixedDeltaTime);
    }

    public void ResetFallVelocity()
    {
        m_Velocity.y = 0f;
    }

    public void AddJumpImpulse()
    {
        m_Velocity.y += Mathf.Sqrt(m_JumpHeight * -3.0f * m_GravityValue);
    }

    public void AddImpulse(Vector3 impulse)
    {
        m_Velocity += impulse;
    }

    private float UpdateSprintModifier(bool isMoving)
    {
        bool doSprint = m_CanSprint && Input.GetButton("Sprint");
        bool isSprinting = false;
        if (!doSprint)
        {
            m_SprintGauge += (isMoving ? m_WalkSprintGaugeFillRate : m_IdleSprintGaugeFillRate) * Time.fixedDeltaTime;
            m_SprintGauge = Mathf.Clamp01(m_SprintGauge);
            m_SprintGaugeChannel.RaiseValueChanged(m_SprintGauge, !m_CanSprint);
            if (m_SprintGauge >= 1.0f)
            {
                m_CanSprint = true;
            }
        }
        else if (isMoving)
        {
            if (m_SprintGauge > 0)
            {
                isSprinting = true;
                m_SprintGauge -= m_SprintGaugeDecelerationRate * Time.fixedDeltaTime;
                m_SprintGauge = Mathf.Clamp01(m_SprintGauge);
                m_SprintGaugeChannel.RaiseValueChanged(m_SprintGauge, !m_CanSprint);
            }
            else
            {
                m_CanSprint = false;
            }
        }

        m_SpeedGauge.RaiseValueChanged(isSprinting ? 10.0f : 0.0f, false);

        return isSprinting ? m_RunSpeedMultiplier : 1.0f;
    }
}
