using UnityEngine;

public class FragileGround : MonoBehaviour
{
    private enum State
    {
        Idle,
        Crumbling,
        ParticleAnimation,
        Destroyed
    }

    [SerializeField]
    private float m_CrumbleDuration = 1.0f;
    [SerializeField]
    private float m_ParticleDuration = 1.0f;
    [SerializeField]
    private Collider m_Collider;
    [SerializeField]
    private MeshRenderer m_MeshRenderer;
    [SerializeField]
    private ParticleSystem m_ParticleSystem;

    private State m_State = State.Idle;
    private float m_TimerStart = 0.0f;
    private System.Random m_RandomGenerator = new System.Random();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            m_State = State.Crumbling;
            m_TimerStart = Time.time;
        }
    }

    private void Update()
    {
        switch (m_State)
        {
            case State.Crumbling:
            {
                    Vector3 offset = new Vector3
                        (
                            m_RandomGenerator.Next(100) / 1000.0f,
                            m_RandomGenerator.Next(100) / 1000.0f,
                            m_RandomGenerator.Next(100) / 1000.0f
                        );
                m_MeshRenderer.transform.localPosition = offset;

                if (Time.time > m_TimerStart + m_CrumbleDuration)
                {
                    m_Collider.enabled = false;
                    m_MeshRenderer.enabled = false;
                    m_TimerStart = Time.time;
                    m_ParticleSystem.Play();
                    m_State = State.ParticleAnimation;
                }
                break;
            }

            case State.ParticleAnimation:
            {
                if (Time.time > m_TimerStart + m_ParticleDuration)
                {
                    m_State = State.Destroyed;
                }
                break;
            }

            case State.Destroyed:
            {
                Destroy(gameObject);
                break;
            }
        }
    }
}