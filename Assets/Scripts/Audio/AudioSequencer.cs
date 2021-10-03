using UnityEngine;

public class AudioSequencer : MonoBehaviour
{
    [SerializeField]
    private AudioChannel m_AudioChannel;

    private AudioSource m_AudioSource;
    private AudioPriority m_CurrentClipPriority = AudioPriority.Low;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

        m_AudioChannel.OnPlayAudioRequested += OnPlayAudioRequested;
        m_AudioChannel.OnStopAudioRequested += OnStopAudioRequested;
    }

    private void OnDestroy()
    {
        m_AudioChannel.OnPlayAudioRequested -= OnPlayAudioRequested;
        m_AudioChannel.OnStopAudioRequested -= OnStopAudioRequested;
    }

    private void OnPlayAudioRequested(AudioClip clip, AudioPriority priority)
    {
        if (!m_AudioSource.isPlaying || (int)priority <= (int)m_CurrentClipPriority && m_AudioSource.clip != clip)
        {
            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }

            m_CurrentClipPriority = priority;
            m_AudioSource.clip = clip;

            m_AudioSource.Play();
        }
    }

    private void OnStopAudioRequested()
    {
        if (m_AudioSource.isPlaying)
        {
            m_CurrentClipPriority = AudioPriority.Low;
            m_AudioSource.Stop();
        }
    }
}
