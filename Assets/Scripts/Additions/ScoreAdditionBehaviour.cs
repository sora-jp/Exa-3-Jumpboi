using UnityEngine;

public class ScoreAdditionBehaviour : AdditionBehaviour
{
    public string collectAudioEffect;
    public string scoreId;
    public int scoreToAdd;
    public ParticleSystem onCollectedParticlesPrefab;

    protected override void OnPlayerCollision(Player player)
    {
        AudioManager.PlayEffect(collectAudioEffect);
        ScoreManager.Instance.AddScore(scoreId, scoreToAdd);
        if (onCollectedParticlesPrefab != null) Instantiate(onCollectedParticlesPrefab, transform.position, Quaternion.identity).Play();
        Destroy(gameObject);
    }

    public override void PositionOnPlatform(Transform platform)
    {
        transform.position = platform.position + Vector3.up * (1 + 2f/16); // 18 pixels, 1 unit is 16 px. This makes coins not overlap with the jump platforms
    }
}
