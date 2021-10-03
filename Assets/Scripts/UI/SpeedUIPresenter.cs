using UnityEngine;

public class SpeedUIPresenter : MonoBehaviour
{
    [SerializeField]
    private Transform m_ImageTransform;
    [SerializeField]
    private GaugeChannel m_SpeedGauge;
    [SerializeField]
    private AudioChannel m_AudioChannel;
    [SerializeField]
    private AudioClip m_SpeedClip;
    [SerializeField]
    private float m_FastSpeedThreashold = 5.0f;

    private bool m_IsFast = false;
    private System.Random m_RandomGenerator = new System.Random();

    private void Start()
    {
        m_SpeedGauge.OnGaugeValueChanged += OnGaugeValueChanged;
    }

    private void OnDestroy()
    {
        m_SpeedGauge.OnGaugeValueChanged -= OnGaugeValueChanged;
    }

    private void Update()
    {
        if (m_IsFast)
        {
            m_ImageTransform.localPosition = new Vector3
                (
                    m_RandomGenerator.Next(10) - 5,
                    m_RandomGenerator.Next(10) - 5,
                    m_RandomGenerator.Next(10) - 5
                );
        }
    }

    private void OnGaugeValueChanged(float value, bool isDepleted)
    {
        bool wasFast = m_IsFast;
        m_IsFast = value > m_FastSpeedThreashold;

        if (wasFast != m_IsFast)
        {
            m_ImageTransform.gameObject.SetActive(m_IsFast);

            if (m_IsFast)
            {
                m_AudioChannel.RaisePlayAudioRequest(m_SpeedClip);
            }
            else
            {
                m_AudioChannel.RaiseStopAudioRequest();
            }
        }
    }
}
