using UnityEngine;

public enum AudioPriority
{
    Critical,
    High,
    Medium,
    Low
}

[CreateAssetMenu(menuName = "Scriptable Object/Audio/Audio Channel")]
public class AudioChannel : ScriptableObject
{
    public delegate void AudioPlayCallback(AudioClip clip, AudioPriority priority);
    public AudioPlayCallback OnPlayAudioRequested;
    public delegate void AudioStopCallback();
    public AudioStopCallback OnStopAudioRequested;

    public void RaisePlayAudioRequest(AudioClip clip)
    {
        OnPlayAudioRequested?.Invoke(clip, AudioPriority.Medium);
    }

    public void RaisePlayAudioRequest(AudioClip clip, AudioPriority priority)
    {
        OnPlayAudioRequested?.Invoke(clip, priority);
    }

    public void RaiseStopAudioRequest()
    {
        OnStopAudioRequested?.Invoke();
    }
}
