using System.Collections;
using UnityEngine;

public class PauseUIPresenter : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;
    [SerializeField]
    private CanvasGroup m_HUD;
    [SerializeField]
    private CanvasGroup m_PauseScreen;
    [SerializeField]
    private float m_HideAnimationDuration = 0.2f;
    [SerializeField]
    private float m_ShowAnimationDuration = 0.5f;

    private bool m_IsPaused = false;

    private void Start()
    {
        m_FlowChannel.OnRequestTogglePause += OnRequestTogglePause;
    }

    private void OnDestroy()
    {
        m_FlowChannel.OnRequestTogglePause -= OnRequestTogglePause;
    }

    private void OnRequestTogglePause()
    {
        m_IsPaused = !m_IsPaused;
        if (m_IsPaused)
        {
            StartCoroutine(UIUtils.HideUIElement(m_HUD, m_HideAnimationDuration));
            StartCoroutine(UIUtils.ShowUIElement(m_PauseScreen, m_ShowAnimationDuration));
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(UIUtils.ShowUIElement(m_HUD, m_HideAnimationDuration));
            StartCoroutine(UIUtils.HideUIElement(m_PauseScreen, m_ShowAnimationDuration));
        }
    }
}
