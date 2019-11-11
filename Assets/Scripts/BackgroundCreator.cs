using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCreator : MonoBehaviour
{
    public Transform tracked;
    public GameObject bgRow;

    public float spaceBetween = 1;
    public float startY = -8;
    public float ceiling = 8;

    GameObject m_lastRow;

    void Awake()
    {
        m_lastRow = SpawnRow(startY);
    }

    void Update()
    {
        while (m_lastRow.transform.position.y < ceiling + tracked.position.y)
        {
            m_lastRow = SpawnRow(m_lastRow.transform.position.y + spaceBetween);
        }
    }

    GameObject SpawnRow(float y)
    {
        GameObject newRow = Instantiate(bgRow, transform);
        newRow.transform.position = Vector3.up * y;
        return newRow;
    }
}
