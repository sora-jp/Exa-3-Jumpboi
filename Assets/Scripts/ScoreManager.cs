using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the score for the current run.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton boilerplate
    public int CurrentScore => m_scoreIdToScore.Sum(v => v.Value); // Sum just sums, check out LINQ if you're curious
    Dictionary<string, int> m_scoreIdToScore;

    // The scores work like this:
    // The final score is split into multiple sub-scores, which can be individually modified.
    // For example, one entry might represent the score gotten from coins, while the other might represent the height the player has gained.
    // This is because for certain score types it is useful if i could directly set them, and not disturb the actual total.

    void Awake()
    {
        // Singleton boilerplate
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        m_scoreIdToScore = new Dictionary<string, int>();
    }

    // Set the value with the id to the score.
    public void SetScore(string id, int score)
    {
        m_scoreIdToScore[id] = score;
    }

    // Adds an amount to score with the specified id
    public void AddScore(string id, int score)
    {
        if (m_scoreIdToScore.ContainsKey(id)) m_scoreIdToScore[id] += score;
        else SetScore(id, score);
    }
}