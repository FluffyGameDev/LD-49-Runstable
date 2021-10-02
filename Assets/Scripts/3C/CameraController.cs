using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector2 m_MouseMouvementMutiplier;
    [SerializeField]
    float m_LookHeightLimit;

    float m_RotationX = 0.0f;
    private Transform m_ParentTransform;
    private void Start()
    {
        m_ParentTransform = transform.parent;
    }

    private void FixedUpdate()
    {
        Vector2 movement = Time.fixedDeltaTime * m_MouseMouvementMutiplier * new Vector2
            (
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            );

        m_RotationX += -movement.y;
        m_RotationX = Mathf.Clamp(m_RotationX, -m_LookHeightLimit, m_LookHeightLimit);

        transform.localRotation = Quaternion.Euler(m_RotationX, 0, 0);

        m_ParentTransform.forward = Quaternion.Euler(0, movement.x, 0) * m_ParentTransform.forward;
    }
}
