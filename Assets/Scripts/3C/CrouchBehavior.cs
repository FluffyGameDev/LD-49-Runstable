using UnityEngine;

public class CrouchBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_DefaultHeight = 2.0f;
    [SerializeField]
    private float m_CrouchHeight = 2.0f;
    [SerializeField]
    private float m_CrouchStopHeight = 2.1f;
    [SerializeField]
    private LayerMask m_WallLayer;

    private CharacterController m_CharacterController;
    private bool m_IsCrouching = false;

    public bool IsCrouching => m_IsCrouching;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        bool shouldCrouch = Input.GetButton("Crouch");
        if (shouldCrouch && !m_IsCrouching)
        {
            StartCrouch();
        }
        else if (!shouldCrouch && m_IsCrouching && CanStopCrouch())
        {
            StopCrouch();
        }
    }

    private void StartCrouch()
    {
        m_CharacterController.height = m_CrouchHeight;
        m_IsCrouching = true;
    }

    private void StopCrouch()
    {
        m_CharacterController.height = m_DefaultHeight;
        m_IsCrouching = false;
    }

    private bool CanStopCrouch()
    {
        RaycastHit hit;
        return !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, m_CrouchStopHeight, m_WallLayer.value);
    }
}
