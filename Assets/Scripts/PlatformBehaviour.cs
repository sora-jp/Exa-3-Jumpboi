using UnityEngine;

public abstract class PlatformBehaviour : MonoBehaviour
{
    public abstract void OnPlayerCollision(Player player);
}