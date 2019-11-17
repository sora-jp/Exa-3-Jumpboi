/// <summary>
/// A leaderboard score
/// </summary>
public struct Score
{
    public string name; // Person who set the score
    public int score; // Score amount
    public bool isCurrent; // Was this score set this run?

    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
        isCurrent = false;
    }
}