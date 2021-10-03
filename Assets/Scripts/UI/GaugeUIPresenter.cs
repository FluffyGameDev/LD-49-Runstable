using UnityEngine;
using UnityEngine.UI;

public class GaugeUIPresenter : MonoBehaviour
{
    [SerializeField]
    Image m_GaugeValue;
    [SerializeField]
    GaugeChannel m_GaugeChannel;
    [SerializeField]
    Color m_DefaultColor;
    [SerializeField]
    Color m_DepletedColor;

    private void Start()
    {
        m_GaugeChannel.OnGaugeValueChanged += OnGaugeValueChanged;
    }

    private void OnDestroy()
    {
        m_GaugeChannel.OnGaugeValueChanged -= OnGaugeValueChanged;
    }

    private void OnGaugeValueChanged(float gaugeValue, bool isDepleted)
    {
        Vector3 scale = m_GaugeValue.transform.localScale;
        scale.x = Mathf.Clamp01(gaugeValue);
        m_GaugeValue.transform.localScale = scale;

        m_GaugeValue.color = (isDepleted ? m_DepletedColor : m_DefaultColor);
    }
}