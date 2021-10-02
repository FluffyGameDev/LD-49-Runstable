using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Impluse;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.AddImpulse(m_Impluse);
        }
    }
}
