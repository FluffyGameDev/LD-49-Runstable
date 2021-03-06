using UnityEngine;
using UnityEngine.Events;

public class EndLevelUIPresenter : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;
    [SerializeField]
    private AudioChannel m_AudioChannel;
    [SerializeField]
    private AudioClip m_LevelEndClip;
    [SerializeField]
    private CanvasGroup m_HUD;
    [SerializeField]
    private CanvasGroup m_EndLevelScreen;
    [SerializeField]
    private GameObject m_BackButton;
    [SerializeField]
    private float m_HideAnimationDuration = 0.5f;
    [SerializeField]
    private float m_ShowAnimationDuration = 1.0f;

    private Coroutine m_HUDCoroutine = null;
    private Coroutine m_EndLevelCoroutine = null;

    private void Start()
    {
        m_FlowChannel.OnLevelComplete += OnLevelComplete;
    }

    private void OnDestroy()
    {
        m_FlowChannel.OnLevelComplete -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        m_AudioChannel.RaisePlayAudioRequest(m_LevelEndClip, AudioPriority.High);

        LevelData nextLevel = LevelLoader.Instance.CurrentLevel;
        m_BackButton.SetActive(nextLevel != null && nextLevel.NextLevel != null);

        StopAnimations();
        m_HUDCoroutine = StartCoroutine(UIUtils.HideUIElement(m_HUD, m_HideAnimationDuration));
        m_EndLevelCoroutine = StartCoroutine(UIUtils.ShowUIElement(m_EndLevelScreen, m_ShowAnimationDuration));
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StopAnimations();
        m_HUDCoroutine = StartCoroutine(UIUtils.ShowUIElement(m_HUD, m_HideAnimationDuration));
        m_EndLevelCoroutine = StartCoroutine(UIUtils.HideUIElement(m_EndLevelScreen, m_ShowAnimationDuration));
    }

    private void StopAnimations()
    {
        if (m_HUDCoroutine != null)
        {
            StopCoroutine(m_HUDCoroutine);
        }
        if (m_EndLevelCoroutine != null)
        {
            StopCoroutine(m_EndLevelCoroutine);
        }
    }
}
