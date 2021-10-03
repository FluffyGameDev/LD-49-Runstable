using UnityEngine;
using UnityEngine.Events;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Impluse;
    [SerializeField]
    private UnityEvent m_OnJump;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Animation animation = GetComponent<Animation>();
            if (animation != null)
            {
                animation.Play();
            }

            player.AddImpulse(m_Impluse);
            m_OnJump?.Invoke();
        }
    }
}
