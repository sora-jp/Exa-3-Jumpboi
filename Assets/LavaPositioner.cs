using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPositioner : MonoBehaviour
{
    public Transform tracked;
    public float heightOffset;

    float m_maxY;

    void Awake()
    {
        m_maxY = tracked.position.y;
    }

    void Update()
    {
        m_maxY = Mathf.Max(tracked.position.y, m_maxY);
        transform.position = Vector3.up * (m_maxY - heightOffset);
    }
}
