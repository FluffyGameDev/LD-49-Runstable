using System.Collections;
using UnityEngine;

public class GameOverUIPresenter : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;
    [SerializeField]
    private CanvasGroup m_HUD;
    [SerializeField]
    private CanvasGroup m_GameOverScreen;
    [SerializeField]
    private float m_HideAnimationDuration = 0.5f;
    [SerializeField]
    private float m_ShowAnimationDuration = 1.0f;

    private void Start()
    {
        m_FlowChannel.OnRequestGameOver += OnRequestGameOver;
    }

    private void OnDestroy()
    {
        m_FlowChannel.OnRequestGameOver -= OnRequestGameOver;
    }

    private void OnRequestGameOver()
    {
        StartCoroutine(UIUtils.HideUIElement(m_HUD, m_HideAnimationDuration));
        StartCoroutine(UIUtils.ShowUIElement(m_GameOverScreen, m_ShowAnimationDuration));
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(UIUtils.ShowUIElement(m_HUD, m_HideAnimationDuration));
        StartCoroutine(UIUtils.HideUIElement(m_GameOverScreen, m_ShowAnimationDuration));
    }
}
