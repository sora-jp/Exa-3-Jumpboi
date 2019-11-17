using UnityEngine;

/// <summary>
/// Base class for addition components. Each addition needs one of these components
/// Responsible for getting collision events, and positioning the addition relative to the platform
/// </summary>
public abstract class AdditionBehaviour : MonoBehaviour
{
    // ReSharper disable once SuggestBaseTypeForParameter, because unity message
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // The player collided with us. Call the OnPlayerCollision method so that any additions can override and get a notification.
            OnPlayerCollision(other.transform.GetComponent<Player>());
        }
    }

    protected abstract void OnPlayerCollision(Player player);  // Called when we collide with the player
    public abstract void PositionOnPlatform(Transform platform);  // Responsible for placing the addition on a platform.
}
