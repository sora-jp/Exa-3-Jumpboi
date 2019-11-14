using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore => _scoreIdToScore.Sum(v => v.Value);

    Dictionary<string, int> _scoreIdToScore;

    public static event Action<Score[]> OnScoresUpdated;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Two ScoreManagers in scene. This is not allowed");
            Destroy(this);
            return;
        }

        Instance = this;
        _scoreIdToScore = new Dictionary<string, int>();
    }

    public void SetScore(string id, int score)
    {
        _scoreIdToScore[id] = score;
    }

    public void AddScore(string id, int score)
    {
        if (_scoreIdToScore.ContainsKey(id)) _scoreIdToScore[id] += score;
        else SetScore(id, score);
    }

    public void SubmitCurrentScore(string playerName)
    {

    }
}

public struct Score
{

}