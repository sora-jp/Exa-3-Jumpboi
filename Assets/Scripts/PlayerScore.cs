using UnityEngine;

/// <summary>
/// Tracks the score gained when the player gains height.
/// </summary>
public class PlayerScore : MonoBehaviour
{
    public string scoreId; // The id for the score given by this
    public float scorePerUnit = 1; // Score per unit traveled by player

    float m_maxY; // Player max y pos

    void Update()
    {
        m_maxY = Mathf.Max(m_maxY, transform.position.y); // Update max y

        // Set the current score
        ScoreManager.Instance.SetScore(scoreId, (int)(m_maxY * scorePerUnit));
    }
}
