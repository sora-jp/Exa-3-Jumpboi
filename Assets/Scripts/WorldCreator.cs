using System.Linq;
using UnityEngine;

/// <summary>
/// Spawns the world. That's it.
/// </summary>
public class WorldCreator : MonoBehaviour
{
    [Header("Scene-specific information")]
    public Transform tracked; // The transform to track height from (probs player)
    public GameObject firstPlatform; // Reference to the first platform (big boi on bottom)

    [Header("Objects")]
    public Platform[] platforms; // All possible platforms
    public Addition[] additions; // All possible additions

    [Header("Spawning properties")]
    public float ceiling; // How many units above player to stop spawning the world
    public float platformXRange; // X range around center to spawn platforms (to have padding at the sides)
    public float platformMinYDst; // Min y distance from previous platform to next
    public float platformMaxYDst; // Max y distance from previous platform to next

    Transform m_lastPlatform; // Last spawned platform

    void Awake()
    {
        m_lastPlatform = firstPlatform.transform; //* Biblical reference (Matthew 20:16) *//
    }

    void Update()
    {
        // Spawn platforms + additions while we haven't reached the ceiling yet.
        while (m_lastPlatform.position.y < ceiling + tracked.position.y)
        {
            m_lastPlatform = SpawnPlatform().transform;
            SpawnAdditionsForPlatform(m_lastPlatform);
        }
    }

    // Spawn a single random platform
    GameObject SpawnPlatform()
    {
        var platform = Instantiate(GetRandomPlatform().platform, transform);

        // Place the y relative to the last platforms position, and the x should be random in the platformXRange range
        Vector2 pos = m_lastPlatform.transform.position;
        pos.x = Random.Range(-platformXRange, platformXRange);
        pos.y += Random.Range(platformMinYDst, platformMaxYDst);

        // FUS ROH DAAAAH
        platform.transform.position = pos;

        return platform.gameObject;
    }

    // Spawns a random addition for a platform (might not spawn any addition at all)
    void SpawnAdditionsForPlatform(Transform platform)
    {
        foreach (var addition in additions)
        {
            // Lower spawn chance = higher chance of failiure. Failiure = random >= spawnchance
            if (Random.value >= addition.spawnChance) continue;

            // Addition passed randomness. Spawn it, then return (platforms can only have 1 addition on them)
            SpawnAddition(addition.addition, platform);
            return;
        }
    }

    // Spawns a single addition
    void SpawnAddition(AdditionBehaviour additionPrefab, Transform platform)
    {
        // Create prefab, and position it on the current platform.
        var additionInstance = Instantiate(additionPrefab, transform);
        additionInstance.transform.position = platform.position;

        // The AdditionBehaviour can position the addition relative to the platform, but an AdditionPositionOverrideBehaviour will position it instead if that exists
        // This was needed because i wanted to have the same base addition script, but different positioning behaviours
        var overridePosition = additionInstance.GetComponent<AdditionPositionOverrideBehaviour>();

        // Actually position the addition
        if (overridePosition != null) overridePosition.PositionOnPlatform(platform);
        else additionInstance.PositionOnPlatform(platform);
    }

    // Get a random platform
    // Basically simulates a bag, where the amount of platforms in the bag is equal to platform.spawnChance
    // Then, picks one random platform out of said bag, and returns it.
    // Note, bag is not persistent (same probabilities no matter what platforms came before)
    Platform GetRandomPlatform()
    {
        var sumChance = platforms.Sum(i => i.spawnChance);
        var chance = Random.Range(0, sumChance);

        float curChance = 0;

        foreach (var t in platforms)
        {
            curChance += t.spawnChance;
            if (curChance > chance) return t;
        }

        return default;
    }
}