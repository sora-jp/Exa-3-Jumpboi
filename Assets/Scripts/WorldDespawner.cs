using UnityEngine;

/// <summary>
/// Despawns the world once it reaches a certain number of units below the player
/// </summary>
public class WorldDespawner : MonoBehaviour
{
    public Transform tracked; // The transform to track position from (player)
    public float cutoff = 20; // Distance below player where we destroy stuff

    void Update()
    {
        // Reversed for-loop, because when we delete a child all the children behind it get moved forward in the list.
        // That means that we would have had to update the index when we destroy a child.
        // I just reversed the for-loop instead.
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            // Destroy child if it is below the cutoff value.
            var child = transform.GetChild(i);
            if (child.position.y < tracked.transform.position.y - cutoff) Destroy(child.gameObject);
        }
    }
}
