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
    [SerializeField]
    private Sprite m_LockedLevelSprite;


    public void RefreshLevels()
    {
        foreach (Transform child in m_LevelsObject)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (LevelData level in LevelLoader.Instance.AllLevels)
        {
            bool isUnlocked = index < LevelLoader.Instance.UnlockedLevelCount;

            Button newButton = Instantiate(m_LevelButtonPrefab, m_LevelsObject);
            newButton.interactable = isUnlocked;
            newButton.onClick.AddListener(() => m_FlowChannel.RaiseRequestLoadLevel(level));

            Text levelNameText = newButton.GetComponentInChildren<Text>();
            Image levelImage = null;

            //I despise this...
            foreach (Transform child in newButton.transform)
            {
                levelImage = child.GetComponent<Image>();
                if (levelImage != null) break;
            }

            if (isUnlocked)
            {
                levelNameText.text = level.LevelName;
                levelImage.sprite = level.LevelThumbnail;
            }
            else
            {
                levelNameText.text = "Locked";
                levelImage.sprite = m_LockedLevelSprite;
            }

            ++index;
        }
    }
}
