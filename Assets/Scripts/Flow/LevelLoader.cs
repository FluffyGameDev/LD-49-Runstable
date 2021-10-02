using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;
    [SerializeField]
    private int m_TitleScreenSceneIndex;
    [SerializeField]
    private int m_HUDSceneIndex;
    [SerializeField]
    private int m_GameCompleteSceneIndex;
    [SerializeField]
    private LevelData m_FirstLevelData;

    private List<LevelData> m_AllLevels = new List<LevelData>();
    public List<LevelData> AllLevels => m_AllLevels;
    public int UnlockedLevelCount => m_SaveData.UnlockedLevelCount;


    private LevelData m_CurrentLevel;
    public LevelData CurrentLevel => m_CurrentLevel;

    private static LevelLoader ms_Instance;
    public static LevelLoader Instance => ms_Instance;

    private LevelSaveData m_SaveData;

    private void Start()
    {
        ms_Instance = this;

        m_FlowChannel.OnLevelLoadRequest += OnLevelLoadRequest;
        m_FlowChannel.OnRequestLoadNextLevel += OnRequestLoadNextLevel;
        m_FlowChannel.OnRequestGoToMainMenu += OnRequestGoToMainMenu;
        m_FlowChannel.OnRequestRestartCurrentLevel += OnRequestRestartCurrentLevel;
        m_FlowChannel.OnRequestApplicationQuit += OnRequestApplicationQuit;
        m_FlowChannel.OnLevelComplete += OnLevelComplete;

        m_FlowChannel.RaiseRequestGoToMainMenu();

        m_SaveData = LevelSaveData.LoadData();

        m_AllLevels.Clear();
        LevelData level = m_FirstLevelData;
        while (level != null)
        {
            m_AllLevels.Add(level);
            level = level.NextLevel;
        }
    }

    private void OnDestroy()
    {
        m_FlowChannel.OnLevelLoadRequest -= OnLevelLoadRequest;
        m_FlowChannel.OnRequestLoadNextLevel -= OnRequestLoadNextLevel;
        m_FlowChannel.OnRequestGoToMainMenu -= OnRequestGoToMainMenu;
        m_FlowChannel.OnRequestRestartCurrentLevel -= OnRequestRestartCurrentLevel;
        m_FlowChannel.OnRequestApplicationQuit -= OnRequestApplicationQuit;
        m_FlowChannel.OnLevelComplete -= OnLevelComplete;
    }

    private void OnLevelLoadRequest(LevelData level)
    {
        if (m_CurrentLevel != null)
        {
            SceneManager.UnloadSceneAsync(m_CurrentLevel.SceneIndex);
        }
        else
        {
            SceneManager.UnloadSceneAsync(m_TitleScreenSceneIndex);
        }

        m_CurrentLevel = level;

        SceneManager.LoadSceneAsync(m_CurrentLevel.SceneIndex, LoadSceneMode.Additive);
        UpdateCursorMode();
        UpdateHUDLoading();
    }

    private void OnRequestLoadNextLevel()
    {
        if (m_CurrentLevel != null)
        {
            SceneManager.UnloadSceneAsync(m_CurrentLevel.SceneIndex);

            m_CurrentLevel = m_CurrentLevel.NextLevel;

            if (m_CurrentLevel != null)
            {
                SceneManager.LoadSceneAsync(m_CurrentLevel.SceneIndex, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadSceneAsync(m_GameCompleteSceneIndex, LoadSceneMode.Additive);
            }
        }
        UpdateCursorMode();
        UpdateHUDLoading();
    }

    private void OnRequestGoToMainMenu()
    {
        if (m_CurrentLevel != null)
        {
            SceneManager.UnloadSceneAsync(m_CurrentLevel.SceneIndex);

            m_CurrentLevel = null;
        }
        else
        {
            if (IsSceneLoaded(m_GameCompleteSceneIndex))
            {
                SceneManager.UnloadSceneAsync(m_GameCompleteSceneIndex);
            }
        }

        SceneManager.LoadSceneAsync(m_TitleScreenSceneIndex, LoadSceneMode.Additive);
        UpdateCursorMode();
        UpdateHUDLoading();
    }

    private void OnRequestRestartCurrentLevel()
    {
        if (m_CurrentLevel != null)
        {
            SceneManager.UnloadSceneAsync(m_CurrentLevel.SceneIndex);
            SceneManager.LoadSceneAsync(m_CurrentLevel.SceneIndex, LoadSceneMode.Additive);
        }
    }

    private void OnRequestApplicationQuit()
    {
        Application.Quit();
    }

    private void OnLevelComplete()
    {
        int currentLevelIndex = m_AllLevels.IndexOf(m_CurrentLevel);
        if (currentLevelIndex == m_SaveData.UnlockedLevelCount - 1)
        {
            ++m_SaveData.UnlockedLevelCount;
        }
        LevelSaveData.SaveData(m_SaveData);
    }

    private void UpdateCursorMode()
    {
        Cursor.lockState = m_CurrentLevel != null ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private bool IsSceneLoaded(int sceneIndex)
    {
        bool isLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).buildIndex == sceneIndex)
            {
                isLoaded = true;
                break;
            }
        }
        return isLoaded;
    }

    private void UpdateHUDLoading()
    {
        bool isHUDLoaded = IsSceneLoaded(m_HUDSceneIndex);
        if (m_CurrentLevel != null && !isHUDLoaded)
        {
            SceneManager.LoadSceneAsync(m_HUDSceneIndex, LoadSceneMode.Additive);
        }
        else if (m_CurrentLevel == null && isHUDLoaded)
        {
            SceneManager.UnloadSceneAsync(m_HUDSceneIndex);
        }
    }
}