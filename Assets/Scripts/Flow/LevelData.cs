using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Flow/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int m_SceneIndex;
    [SerializeField]
    private string m_LevelName;
    [SerializeField]
    private Sprite m_LevelThumbnail;
    [SerializeField]
    private LevelData m_NextLevel;

    public int SceneIndex => m_SceneIndex;
    public LevelData NextLevel => m_NextLevel;
    public string LevelName => m_LevelName;
    public Sprite LevelThumbnail => m_LevelThumbnail;
}
