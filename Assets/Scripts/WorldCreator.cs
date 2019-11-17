using System.Linq;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    // ReSharper disable InconsistentNaming
    #pragma warning disable 649
    
    [Header("Scene-specific information")]
    [SerializeField] Transform tracked;
    [SerializeField] GameObject firstPlatform;

    [Header("Objects")]
    [SerializeField] Platform[] platforms;
    [SerializeField] Addition[] additions;

    [Header("Spawning properties")]
    [SerializeField] float ceiling;
    [SerializeField] float platformXRange;
    [SerializeField] float platformMinYDst;
    [SerializeField] float platformMaxYDst;
    
    #pragma warning restore 649
    // ReSharper restore InconsistentNaming

    Transform m_lastPlatform; // Last spawned platform

    void Awake()
    {
        m_lastPlatform = firstPlatform.transform;
    }

    void Update()
    {
        while (m_lastPlatform.position.y < ceiling + tracked.position.y)
        {
            m_lastPlatform = SpawnPlatform().transform;
            SpawnAdditionsForPlatform(m_lastPlatform);
        }
    }

    GameObject SpawnPlatform()
    {
        var platform = Instantiate(GetRandomPlatform().platform, transform);

        Vector2 pos = m_lastPlatform.transform.position;
        pos.x = Random.Range(-platformXRange, platformXRange);
        pos.y += Random.Range(platformMinYDst, platformMaxYDst);

        platform.transform.position = pos;

        return platform.gameObject;
    }

    void SpawnAdditionsForPlatform(Transform platform)
    {
        foreach (var addition in additions)
        {
            if (Random.value >= addition.spawnChance) continue;

            SpawnAddition(addition.addition, platform);
            break;
        }
    }

    void SpawnAddition(AdditionBehaviour addition, Transform platform)
    {
        var additionInstance = Instantiate(addition, transform);
        additionInstance.transform.position = platform.position;
        var overridePosition = additionInstance.GetComponent<AdditionPositionOverrideBehaviour>();
        if (overridePosition != null) overridePosition.PositionOnPlatform(platform);
        else additionInstance.PositionOnPlatform(platform);
    }

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