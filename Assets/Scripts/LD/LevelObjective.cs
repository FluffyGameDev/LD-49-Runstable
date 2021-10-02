using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    [SerializeField]
    private FlowChannel m_FlowChannel;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.enabled = false;
            m_FlowChannel.RaiseLevelComplete();

            CameraController cameraController = other.GetComponentInChildren<CameraController>();
            if (cameraController != null)
            {
                cameraController.enabled = false;
            }

            PauseBehavior pauseBehavior = other.GetComponent<PauseBehavior>();
            if (pauseBehavior != null)
            {
                pauseBehavior.enabled = false;
            }
        }
    }
}
