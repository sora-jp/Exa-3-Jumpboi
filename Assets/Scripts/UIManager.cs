using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int scoreNumDigits;
    public string scoreTextFormat;
    public TextMeshProUGUI scoreText;


    void Update()
    {
        scoreText.text = string.Format(scoreTextFormat,
            ScoreManager.Instance.CurrentScore.ToString("D" + scoreNumDigits));
    }
}
