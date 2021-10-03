using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_OnStart;

    private void Start()
    {
        m_OnStart?.Invoke();
    }
}
