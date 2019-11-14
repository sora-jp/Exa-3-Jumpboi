using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    public ScoreItem itemPrefab;

    ScoreList m_scores;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        m_scores = new ScoreList();
        if (PlayerPrefs.HasKey("scores")) m_scores.FromBase64(PlayerPrefs.GetString("scores"));
    }

    public void SubmitScore(string playerName, int score)
    {
        m_scores.AddScore(new Score(playerName, score) {isCurrent = true});
        PlayerPrefs.SetString("scores", m_scores.ToBase64());
        RespawnScores();
    }

    void RespawnScores()
    {
        Clear();
        SpawnScores();
    }

    void SpawnScores()
    {
        var hasCurrentInTop5 = false;
        for (var i = 0; i < Mathf.Min(m_scores.scores.Count, 5); i++)
        {
            SpawnScore(m_scores.scores[i]);
            hasCurrentInTop5 |= m_scores.scores[i].isCurrent;
        }

        if (!hasCurrentInTop5)
        {
            SpawnScore(m_scores.scores.First(s => s.isCurrent));
        }
    }

    void SpawnScore(Score score)
    {
        var item = Instantiate(itemPrefab, transform);
        item.SetScore(score);
    }

    void Clear()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}