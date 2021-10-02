using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/3C/Gauge Channel")]
public class GaugeChannel : ScriptableObject
{
    public delegate void OnGaugeValueChangedCallback(float gaugeValue);
    public OnGaugeValueChangedCallback OnGaugeValueChanged;

    public void RaiseValueChanged(float gaugeValue)
    {
        OnGaugeValueChanged?.Invoke(gaugeValue);
    }
}