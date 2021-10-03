using UnityEngine;

public class AudioOnStart : MonoBehaviour
{
    [SerializeField]
    private AudioChannel m_AudioChannel;
    [SerializeField]
    private AudioClip m_Clip;
    [SerializeField]
    private AudioPriority m_Priority;

    private void Start()
    {
        m_AudioChannel.RaisePlayAudioRequest(m_Clip, m_Priority);
    }
}
