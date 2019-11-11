using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public string scoreId;
    public float scorePerUnit = 1;

    float m_maxY;

    void Update()
    {
        m_maxY = Mathf.Max(m_maxY, transform.position.y);
        ScoreManager.Instance.SetScore(scoreId, (int)(m_maxY * scorePerUnit));
    }
}
