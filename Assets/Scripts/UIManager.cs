using System.Collections;
using TMPro;
using UnityBase.Animations;
using UnityEngine;

/// <summary>
/// Controls the gameplay ui
/// </summary>
public class UIManager : MonoBehaviour
{
    public int scoreNumDigits; // Number of digits in score display
    public string scoreTextFormat; // Score format (normal C# format string)
    public TextMeshProUGUI scoreText; // Score text
    public TextMeshProUGUI startText; // Press Space To Start text

    void Update()
    {
        startText.gameObject.SetActive(!GameManager.Instance.gameStarted); // Enable the text if game is not started, otherwise disable
        scoreText.text = string.Format(scoreTextFormat,
            ScoreManager.Instance.CurrentScore.ToString("D" + scoreNumDigits)); // Format the base format with the current score, padded with 0 to the specified length
    }
}
