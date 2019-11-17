using UnityEngine;

public class WorldDespawner : MonoBehaviour
{
    public Transform tracked;
    public float cutoff = 20;

    void Update()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            if (child.position.y < tracked.transform.position.y - cutoff) Destroy(child.gameObject);
        }
    }
}
