using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scriptable Object/Flow/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int m_SceneIndex;
    [SerializeField]
    private LevelData m_NextLevel;

    public int SceneIndex => m_SceneIndex;
    public LevelData NextLevel => m_NextLevel;
}
