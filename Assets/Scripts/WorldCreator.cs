using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    public Transform tracked;
    public GameObject firstPlatform;

    public Platform[] platforms;
    public Addition[] additions;

    public float ceiling;

    public float platformXRange;

    public float platformMinYDst;
    public float platformMaxYDst;

    Transform m_lastPlatform;

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
        var platform = Instantiate(PickRandom(platforms).platform, transform);

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

    static void SpawnAddition(AdditionBehaviour addition, Transform platform)
    {
        var additionInstance = Instantiate(addition, platform);
        additionInstance.transform.localPosition = Vector3.zero;
        additionInstance.PositionOnPlatform(platform);
    }

    static T PickRandom<T>(T[] list) where T : IPickRandom
    {
        var sumChance = list.Sum(i => i.GetChance());
        var chance = Random.Range(0, sumChance);

        float curChance = 0;

        foreach (var t in list)
        {
            curChance += t.GetChance();
            if (curChance > chance) return t;
        }

        return default;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.left * platformXRange, Vector3.right * platformXRange);
        Gizmos.DrawLine(Vector3.left, Vector3.left + Vector3.up * platformMinYDst);
        Gizmos.DrawLine(Vector3.right, Vector3.right + Vector3.up * platformMaxYDst);
    }
}