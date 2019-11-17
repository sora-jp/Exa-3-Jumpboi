using UnityEngine;

/// <summary>
/// A platform that drops as soon as the player jumps on it.
/// </summary>
public class DroppingPlatformBehaviour : PlatformBehaviour
{
    // The gravity to apply
    public float gravity = 0;

    bool m_drop; // Should we be dropping
    float m_velY; // Current velocity

    void Update()
    {
        if (!m_drop) return; // Don't do anything if we are not dropping

        // Standard gravitation calculations, move ourselves by the delta time, blah blah blah.
        m_velY -= gravity * Time.deltaTime;
        transform.position += Vector3.up * m_velY * Time.deltaTime;
    }

    public override void OnPlayerCollision(Player player)
    {
        // BEGIN DA DROP
        m_drop = true;
    }
}
