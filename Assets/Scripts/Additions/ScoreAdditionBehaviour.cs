using UnityEngine;

/// <summary>
/// Addition that adds score to the player on touch
/// </summary>
public class ScoreAdditionBehaviour : AdditionBehaviour
{
    public string collectAudioEffect; // Audio effect id to play when collected
    public string scoreId; // Score id to add score to
    public int scoreToAdd; // Amount of score to add
    public ParticleSystem onCollectedParticlesPrefab; // Particles to play on collection

    protected override void OnPlayerCollision(Player player)
    {
        AudioManager.PlayEffect(collectAudioEffect);
        ScoreManager.Instance.AddScore(scoreId, scoreToAdd);

        // Only spawn the particles if they exist lol
        if (onCollectedParticlesPrefab != null) Instantiate(onCollectedParticlesPrefab, transform.position, Quaternion.identity).Play();
        
        // Commit Sudoku
        Destroy(gameObject);
    }

    public override void PositionOnPlatform(Transform platform)
    {
        // Literally only vertical offset boi
        transform.position = platform.position + Vector3.up * (1 + 2f/16); // 18 pixels, 1 unit is 16 px. This makes coins not overlap with the jump platforms
    }
}
