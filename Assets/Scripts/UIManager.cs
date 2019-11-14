using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityBase.Animations;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public int scoreNumDigits;
    public string scoreTextFormat;
    public TextMeshProUGUI scoreText;

    public float subscreenAnimDuration;

    public CanvasGroup[] subScreens;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        foreach (var screen in subScreens)
        {
            screen.blocksRaycasts = false;
            screen.alpha = 0;
            screen.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        scoreText.text = string.Format(scoreTextFormat,
            ScoreManager.Instance.CurrentScore.ToString("D" + scoreNumDigits));
    }

    public void NavigateToSubscreen(int screenIdx)
    {
        StartCoroutine(_AnimateToSubscreen(screenIdx));
    }

    IEnumerator _AnimateToSubscreen(int screenIdx)
    {
        for (var i = 0; i < subScreens.Length; i++)
        {
            if (i == screenIdx) continue;
            subScreens[i].AnimateAlpha(0, subscreenAnimDuration / 2, EaseMode.Step4);
            subScreens[i].blocksRaycasts = false;
        }

        yield return new WaitForSeconds(subscreenAnimDuration / 2);

        for (var i = 0; i < subScreens.Length; i++)
        {
            if (i == screenIdx) continue;
            subScreens[i].gameObject.SetActive(false);
        }

        subScreens[screenIdx].gameObject.SetActive(true);
        subScreens[screenIdx].AnimateAlpha(1, subscreenAnimDuration / 2, EaseMode.Step4);
        subScreens[screenIdx].blocksRaycasts = true;
    }
}
