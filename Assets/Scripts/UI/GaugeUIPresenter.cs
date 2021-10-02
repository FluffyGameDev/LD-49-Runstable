using UnityEngine;

public class GaugeUIPresenter : MonoBehaviour
{
    [SerializeField]
    Transform m_GaugeValue;
    [SerializeField]
    GaugeChannel m_GaugeChannel;

    private void Start()
    {
        m_GaugeChannel.OnGaugeValueChanged += OnGaugeValueChanged;
    }

    private void OnDestroy()
    {
        m_GaugeChannel.OnGaugeValueChanged -= OnGaugeValueChanged;
    }

    private void OnGaugeValueChanged(float gaugeValue)
    {
        Vector3 scale = m_GaugeValue.localScale;
        scale.x = Mathf.Clamp01(gaugeValue);
        m_GaugeValue.localScale = scale;
    }
}