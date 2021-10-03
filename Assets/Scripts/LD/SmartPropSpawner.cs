using UnityEngine;

public class SmartPropSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_PossibleProps;

    void Start()
    {
        System.Random randomGenerator = new System.Random((int)System.DateTime.Now.Ticks + GetInstanceID());
        Instantiate(m_PossibleProps[randomGenerator.Next(m_PossibleProps.Length)], transform);
    }
}