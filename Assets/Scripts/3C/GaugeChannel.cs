using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/3C/Gauge Channel")]
public class GaugeChannel : ScriptableObject
{
    public delegate void OnGaugeValueChangedCallback(float gaugeValue, bool isDepleted);
    public OnGaugeValueChangedCallback OnGaugeValueChanged;

    public void RaiseValueChanged(float gaugeValue, bool isDepleted)
    {
        OnGaugeValueChanged?.Invoke(gaugeValue, isDepleted);
    }
}