
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 8f;

    private Vector3 followPosition;
    private Vector3 shakeOffset;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        followPosition = Vector3.Lerp(
            followPosition,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = followPosition + shakeOffset;
    }

    public void SetShakeOffset(Vector3 offset)
    {
        shakeOffset = offset;
    }
}
