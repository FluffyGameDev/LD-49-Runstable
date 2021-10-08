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
    private Coroutine m_HUDCoroutine = null;
    private Coroutine m_PauseCoroutine = null;

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
            StopAnimations();
            m_HUDCoroutine = StartCoroutine(UIUtils.HideUIElement(m_HUD, m_HideAnimationDuration));
            m_PauseCoroutine = StartCoroutine(UIUtils.ShowUIElement(m_PauseScreen, m_ShowAnimationDuration));
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            StopAnimations();
            m_HUDCoroutine =StartCoroutine(UIUtils.ShowUIElement(m_HUD, m_HideAnimationDuration));
            m_PauseCoroutine = StartCoroutine(UIUtils.HideUIElement(m_PauseScreen, m_ShowAnimationDuration));
        }
    }

    private void StopAnimations()
    {
        if (m_HUDCoroutine != null)
        {
            StopCoroutine(m_HUDCoroutine);
        }
        if (m_PauseCoroutine != null)
        {
            StopCoroutine(m_PauseCoroutine);
        }
    }
}
