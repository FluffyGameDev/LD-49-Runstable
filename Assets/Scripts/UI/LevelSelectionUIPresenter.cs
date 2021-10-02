using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUIPresenter : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;
    [SerializeField]
    private Button m_LevelButtonPrefab;
    [SerializeField]
    private Transform m_LevelsObject;


    public void RefreshLevels()
    {
        foreach (Transform child in m_LevelsObject)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (LevelData level in LevelLoader.Instance.AllLevels)
        {
            Button newButton = Instantiate(m_LevelButtonPrefab, m_LevelsObject);
            newButton.interactable = index < LevelLoader.Instance.UnlockedLevelCount;
            newButton.onClick.AddListener(() => m_FlowChannel.RaiseRequestLoadLevel(level));
            ++index;
        }
    }
}
