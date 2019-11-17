using UnityEngine;

/// <summary>
/// Base component for a platform. Derived from to create platforms that behave differently
/// </summary>
public abstract class PlatformBehaviour : MonoBehaviour
{
    public abstract void OnPlayerCollision(Player player); // Called when the player collides with us
}