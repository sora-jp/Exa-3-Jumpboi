using UnityEngine;

/// <summary>
/// Kills the player when he contacts our trigger.
/// </summary>
public class KillPlayerOnContact : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for tag
        if (other.CompareTag("Player"))
        {
            // DIE! DIE! DIE!
            other.GetComponent<Player>().KillPlayer();
        }
    }
}
