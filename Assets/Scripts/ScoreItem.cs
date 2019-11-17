using TMPro;
using UnityEngine;

/// <summary>
/// A leaderboard score in the ui
/// </summary>
public class ScoreItem : MonoBehaviour
{
    public Color currentScoreColor; // Color when the score shown by this item is the current score.

    TextMeshProUGUI m_text;

    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(Score score)
    {
        m_text.text = $"{score.name} : {score.score:D8}"; // Nice formatting
        if (score.isCurrent) m_text.color = currentScoreColor; // Change color if this is the current score
    }
}
