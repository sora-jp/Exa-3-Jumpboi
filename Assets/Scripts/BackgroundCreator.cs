using UnityEngine;

/// <summary>
/// Spawns the background tiles (walls)
/// </summary>
public class BackgroundCreator : MonoBehaviour
{
    public Transform tracked; // The transform to base height calculations off of (most likely the player)
    public GameObject bgRow; // Prefab for a single background row

    public float spaceBetween = 1; // Space between the rows
    public float startY = -8; // Start the background spawning at this y position
    public float ceiling = 8; // End spawning this many units above tracked.position

    GameObject m_lastRow; // The last spawned row

    void Awake()
    {
        m_lastRow = SpawnRow(startY); // Spawn the first row, at the starting y pos
    }

    void Update()
    {
        while (m_lastRow.transform.position.y < ceiling + tracked.position.y)
        {
            // Keep spawning rows above the current row until we reach a certain number of units above the tracked position.
            m_lastRow = SpawnRow(m_lastRow.transform.position.y + spaceBetween);
        }
    }

    GameObject SpawnRow(float y)
    {
        // Spawn the row, and set it's position
        var newRow = Instantiate(bgRow, transform);
        newRow.transform.position = Vector3.up * y;
        
        // Return a reference, so that we can use it later.
        return newRow;
    }
}
