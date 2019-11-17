using UnityEngine;

/// <summary>
/// Positions the lava, so that it is always a max distance away from the player, but never moves down.
/// </summary>
public class LavaPositioner : MonoBehaviour
{
    public Transform tracked; // Transform to position relative to (player)
    public float heightOffset; // Offset down to place lava at.

    float m_maxY; // Tracked max y

    void Awake()
    {
        m_maxY = tracked.position.y; // Initial value, just so that we don't need to set it to -infinity or some stupid bs like that
    }

    void Update()
    {
        // Update the max y position
        m_maxY = Mathf.Max(tracked.position.y, m_maxY);
        
        // Position lava
        transform.position = Vector3.up * Mathf.Max(m_maxY - heightOffset, transform.position.y);
    }
}
