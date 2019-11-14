using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int CurrentScore => _scoreIdToScore.Sum(v => v.Value);
    Dictionary<string, int> _scoreIdToScore;

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
}

public class ScoreList
{
    public List<Score> scores = new List<Score>();

    public void FromBase64(string b64)
    {
        scores = new List<Score>();
        using (var memStream = new MemoryStream(Convert.FromBase64String(b64)))
        {
            using (var reader = new BinaryReader(memStream, Encoding.ASCII))
            {
                var len = reader.ReadInt32();
                for (var i = 0; i < len; i++)
                {
                    var name = reader.ReadString();
                    var score = reader.ReadInt32();
                    scores.Add(new Score(name, score));
                }
            }
        }
        SortScores();
    }

    public string ToBase64()
    {
        using (var memStream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(memStream, Encoding.ASCII, true))
            {
                writer.Write(scores.Count);
                foreach (var score in scores)
                {
                    writer.Write(score.name);
                    writer.Write(score.score);
                }
            }

            return Convert.ToBase64String(memStream.ToArray());
        }
    }

    public void AddScore(Score score)
    {
        scores.Add(score);
        SortScores();
    }

    public void SortScores()
    {
        scores.Sort((a, b) => b.score.CompareTo(a.score));
    }
}

public struct Score
{
    public string name;
    public int score;
    public bool isCurrent;

    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
        isCurrent = false;
    }
}