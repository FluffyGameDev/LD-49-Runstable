using UnityEngine;

public class MenuPage : MonoBehaviour
{
    [SerializeField]
    CanvasGroup m_CanvasGroup;
    [SerializeField]
    float m_ShowDuration = 0.5f;
    [SerializeField]
    float m_HideDuration = 0.2f;
    [SerializeField]
    bool m_ShowOnStart = true;

    private void Start()
    {
        if (m_ShowOnStart)
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        StartCoroutine(UIUtils.ShowUIElement(m_CanvasGroup, m_ShowDuration));
    }

    public void HideMenu()
    {
        StartCoroutine(UIUtils.HideUIElement(m_CanvasGroup, m_HideDuration));
    }
}
