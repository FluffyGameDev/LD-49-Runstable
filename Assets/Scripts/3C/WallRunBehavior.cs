using UnityEngine;

public class WallRunBehavior : MonoBehaviour
{
    private enum WallDirection
    {
        Left,
        Right,
        None
    }

    [SerializeField]
    private LayerMask m_WallLayer;
    [SerializeField]
    private float m_WallRunSpeed = 10.0f;
    [SerializeField]
    private GaugeChannel m_SpeedGauge;

    private CharacterController m_CharacterController;
    private PlayerMovement m_PlayerMovement;
    private bool m_IsWallRunning = false;
    private Vector3 m_RunDirection = Vector3.zero;

    public bool IsWallRunning => m_IsWallRunning;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        RunnableWall runnableWall = other.GetComponent<RunnableWall>();
        if (runnableWall != null)
        {
            StartWallRun(runnableWall);
        }
    }

    private void FixedUpdate()
    {
        if (m_IsWallRunning)
        {
            WallDirection wallSide = FindWallDirection();
            if (wallSide == WallDirection.None)
            {
                StopWallRun();
            }
            else if (Input.GetButton("Jump"))
            {
                m_PlayerMovement.AddJumpImpulse();
                StopWallRun();
            }
            else
            {
                m_CharacterController.Move(m_RunDirection * Time.fixedDeltaTime * m_WallRunSpeed);
            }
        }
    }

    private void StartWallRun(RunnableWall runnableWall)
    {
        m_RunDirection = runnableWall.ComputeRunDirection(transform);
        transform.forward = m_RunDirection;
        m_PlayerMovement.ResetFallVelocity();
        m_PlayerMovement.enabled = false;
        m_IsWallRunning = true;
        m_SpeedGauge.RaiseValueChanged(10.0f);
    }

    private void StopWallRun()
    {
        m_PlayerMovement.enabled = true;
        m_IsWallRunning = false;
        m_SpeedGauge.RaiseValueChanged(0.0f);
    }

    private WallDirection FindWallDirection()
    {
        WallDirection direction = WallDirection.None;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1.0f, m_WallLayer.value))
        {
            direction = WallDirection.Left;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1.0f, m_WallLayer.value))
        {
            direction = WallDirection.Right;
        }

        return direction;
    }
}
