using UnityEngine;

public class PauseBehavior : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;

    private CameraController m_CameraController;
    private PlayerMovement m_PlayerMovement;

    private void Start()
    {
        m_CameraController = GetComponentInChildren<CameraController>();
        m_PlayerMovement = GetComponent<PlayerMovement>();

        m_FlowChannel.OnRequestTogglePause += OnRequestTogglePause;
    }

    private void OnDestroy()
    {
        m_FlowChannel.OnRequestTogglePause -= OnRequestTogglePause;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_FlowChannel.RaiseRequestTogglePause();
        }
    }

    private void OnRequestTogglePause()
    {
        m_CameraController.enabled = !m_CameraController.enabled;
        m_PlayerMovement.enabled = m_CameraController.enabled;
    }
}
