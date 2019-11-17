using UnityEngine;

/// <summary>
/// Follows a transform with the camera.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform player; // Transform to follow
    public float smoothTime; // Smooth time, lower time = smoother motion
    public float followCutoff; // Don't follow the transform if it's position is below this value.

    Vector3 m_vel; // Current smoothDamp vel

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref m_vel, smoothTime);
    }

    // Target smooth position
    Vector3 GetTargetPosition()
    {
        // Make sure the camera doesn't go below the followCutoff
        return Vector3.up * Mathf.Max(player.position.y, followCutoff) + Vector3.back * 10;
    }
}
