using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class NameSelector : MonoBehaviour
{
    public Sprite[] characters; // The text characters
    public CharacterSelector[] charSelectors;
    public float animDuration;
    public CanvasGroup leaderBoardGroup;
    public AutoSelect nextSelectable;
    public AutoSelect firstCharSelector;

    CanvasGroup m_group;

    void Awake()
    {
        m_group = GetComponent<CanvasGroup>();
        leaderBoardGroup.alpha = 0;
        nextSelectable.enabled = false;
        firstCharSelector.enabled = true;
    }

    public string GetName()
    {
        return charSelectors.Aggregate("", (current, selector) => current + (char) ('A' + selector.charIdx));
    }

    public void FinishNameInput()
    {
        LeaderboardManager.Instance.SubmitScore(GetName(), ScoreManager.Instance.CurrentScore);
        nextSelectable.enabled = true;
        firstCharSelector.enabled = false;
        StartCoroutine(_AnimateToLeaderboard());
    }

    IEnumerator _AnimateToLeaderboard()
    {
        m_group.AnimateAlpha(0, animDuration / 2, EaseMode.Step4);
        yield return new WaitForSeconds(animDuration / 2);
        leaderBoardGroup.AnimateAlpha(1, animDuration / 2, EaseMode.Step4);
    }
}
