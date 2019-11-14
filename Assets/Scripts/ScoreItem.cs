using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public Color currentScoreColor;

    TextMeshProUGUI m_text;

    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(Score score)
    {
        m_text.text = $"{score.name} : {score.score:D8}";
        if (score.isCurrent) m_text.color = currentScoreColor;
    }
}
