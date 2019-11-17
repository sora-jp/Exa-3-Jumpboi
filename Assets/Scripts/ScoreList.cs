using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
/// <summary>
/// List of leaderboard scores
/// </summary>
public class ScoreList
{
    // BINARY FORMAT:
    // {int count}{string name, int score}*count
    // Speek to me if u no understando

    public List<Score> scores = new List<Score>();

    // Load from a b64 string
    public void FromBase64(string b64)
    {
        // Pretty standard binary manipulation shit
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

    // Save to a b64 string
    public string ToBase64()
    {
        // Pretty standard binary manipulation shit
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

    // Add a new score
    public void AddScore(Score score)
    {
        scores.Add(score);
        SortScores();
    }

    // Sorts scores in a descending order 
    public void SortScores()
    {
        scores.Sort((a, b) => b.score.CompareTo(a.score));
    }
}