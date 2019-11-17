using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the leaderboard. 
/// </summary>
public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; } // Singleton boilerplate

    public ScoreItem itemPrefab; // Score item prefab

    ScoreList m_scores; // The current list of scores.

    void Awake()
    {
        // Singleton boilerplate
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        // Load last scores.
        m_scores = new ScoreList();
        if (PlayerPrefs.HasKey("scores")) m_scores.FromBase64(PlayerPrefs.GetString("scores"));
    }

    // Adds a new score
    public void SubmitScore(string playerName, int score)
    {
        m_scores.AddScore(new Score(playerName, score) {isCurrent = true}); // Add the new score to the list. (also sorts, and marks it as a score gotten this run)
        PlayerPrefs.SetString("scores", m_scores.ToBase64()); // Save to PlayerPrefs
        RespawnScores(); // Reload ui
    }

    // Respawn the UI score representation
    void RespawnScores()
    {
        Clear();
        SpawnScores();
    }

    // Spawn the UI score representation
    void SpawnScores()
    {
        var hasCurrentInTop5 = false; // Did we spawn the current score in the top 5
        for (var i = 0; i < Mathf.Min(m_scores.scores.Count, 5); i++)
        {
            SpawnScore(m_scores.scores[i]);
            hasCurrentInTop5 |= m_scores.scores[i].isCurrent; // If any of the top 5 scores are current, this will be true.
        }

        if (!hasCurrentInTop5)
        {
            // Did not spawn the current score in the top 5. Spawn it as a sixth score instead.
            SpawnScore(m_scores.scores.Single(s => s.isCurrent));
        }
    }

    // Spawn a single score
    void SpawnScore(Score score)
    {
        var item = Instantiate(itemPrefab, transform);
        item.SetScore(score);
    }

    // Remove all children (ui scores)
    void Clear()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject); // Always destroy child 0, because then child 1 becomes child 0, and we destroy that again. It's like a queue.
        }
    }
}