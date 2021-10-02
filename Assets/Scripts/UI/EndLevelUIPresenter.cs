using UnityEngine;

public class EndLevelUIPresenter : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;
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
        LevelData nextLevel = LevelLoader.Instance.CurrentLevel;
        m_BackButton.SetActive(nextLevel != null && nextLevel.NextLevel != null);

        StartCoroutine(UIUtils.HideUIElement(m_HUD, m_HideAnimationDuration));
        StartCoroutine(UIUtils.ShowUIElement(m_EndLevelScreen, m_ShowAnimationDuration));
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(UIUtils.ShowUIElement(m_HUD, m_HideAnimationDuration));
        StartCoroutine(UIUtils.HideUIElement(m_EndLevelScreen, m_ShowAnimationDuration));
    }
}
