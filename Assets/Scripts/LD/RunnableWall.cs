using UnityEngine;

public class RunnableWall : MonoBehaviour
{
    public Vector3 ComputeRunDirection(Transform playerTransform)
    {
        //TODO: do stuff with velocity and angles

        Vector3 direction = transform.forward;
        if (Vector3.Dot(transform.forward, playerTransform.forward) < 0)
        {
            direction = -transform.forward;
        }
        return direction;
    }
}