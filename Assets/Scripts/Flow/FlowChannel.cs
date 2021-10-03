using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Flow/Flow Channel")]
public class FlowChannel : ScriptableObject
{
    public delegate void LevelLoadRequestCallback(LevelData level);
    public LevelLoadRequestCallback OnLevelLoadRequest;

    public delegate void FlowCallback();
    public FlowCallback OnRequestLoadNextLevel;
    public FlowCallback OnRequestGoToMainMenu;
    public FlowCallback OnRequestGameOver;
    public FlowCallback OnRequestRestartCurrentLevel;
    public FlowCallback OnRequestApplicationQuit;
    public FlowCallback OnRequestTogglePause;
    public FlowCallback OnRequestResetProgression;
    public FlowCallback OnLevelComplete;

    public void RaiseRequestLoadLevel(LevelData level)
    {
        OnLevelLoadRequest?.Invoke(level);
    }

    public void RaiseRequestLoadNextLevel()
    {
        OnRequestLoadNextLevel?.Invoke();
    }

    public void RaiseRequestGoToMainMenu()
    {
        OnRequestGoToMainMenu?.Invoke();
    }

    public void RaiseRequestGameOver()
    {
        OnRequestGameOver?.Invoke();
    }

    public void RaiseRequestRestartCurrentLevel()
    {
        OnRequestRestartCurrentLevel?.Invoke();
    }

    public void RaiseRequestApplicationQuit()
    {
        OnRequestApplicationQuit?.Invoke();
    }

    public void RaiseRequestTogglePause()
    {
        OnRequestTogglePause?.Invoke();
    }

    public void RaiseRequestResetProgression()
    {
        OnRequestResetProgression?.Invoke();
    }

    public void RaiseLevelComplete()
    {
        OnLevelComplete?.Invoke();
    }
}
