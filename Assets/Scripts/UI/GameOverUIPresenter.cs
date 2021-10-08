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

    [SerializeField]
    private AudioChannel m_AudioChannel;
    [SerializeField]
    private AudioClip[] m_PossibleClips;

    private Coroutine m_HUDCoroutine = null;
    private Coroutine m_GameOverCoroutine = null;
    private System.Random m_Random = new System.Random();

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
        StopAnimations();
        m_HUDCoroutine = StartCoroutine(UIUtils.HideUIElement(m_HUD, m_HideAnimationDuration));
        m_GameOverCoroutine = StartCoroutine(UIUtils.ShowUIElement(m_GameOverScreen, m_ShowAnimationDuration));
        Cursor.lockState = CursorLockMode.None;

        m_AudioChannel.RaisePlayAudioRequest(m_PossibleClips[m_Random.Next(m_PossibleClips.Length)]);
    }

    public void ShowHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StopAnimations();
        m_HUDCoroutine = StartCoroutine(UIUtils.ShowUIElement(m_HUD, m_HideAnimationDuration));
        m_GameOverCoroutine = StartCoroutine(UIUtils.HideUIElement(m_GameOverScreen, m_ShowAnimationDuration));
    }

    private void StopAnimations()
    {
        if (m_HUDCoroutine != null)
        {
            StopCoroutine(m_HUDCoroutine);
        }
        if (m_GameOverCoroutine != null)
        {
            StopCoroutine(m_GameOverCoroutine);
        }
    }
}
