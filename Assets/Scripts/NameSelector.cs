using System.Collections;
using System.Linq;
using UnityBase.Animations;
using UnityEngine;

/// <summary>
/// Responsible for the process of selecting your name on the death screen, including handoff to the leaderboard after name entered
/// </summary>
public class NameSelector : MonoBehaviour
{
    public Sprite[] characters; // The text characters
    public CharacterSelector[] charSelectors; // All the character selectors (in order)
    public float animDuration;
    public CanvasGroup leaderBoardGroup;
    public AutoSelect nextSelectable;
    public AutoSelect firstCharSelector;

    CanvasGroup m_group;

    void Awake()
    {
        m_group = GetComponent<CanvasGroup>();
        leaderBoardGroup.alpha = 0; // Hide the leaderboard.

        // Make sure only the char selectors can be selected now
        nextSelectable.enabled = false;
        firstCharSelector.enabled = true;
    }

    public string GetName()
    {
        // Sum up all the characters into the name.
        // Aggregate applies an accumulator function
        // We add 'A' to the selector index because it is an index. 0 is A, 1 is B, and so on...
        return charSelectors.Aggregate("", (current, selector) => current + (char) ('A' + selector.charIdx));
    }

    public void FinishNameInput()
    {
        // Hooray, you entered your name! Now we save your score.
        LeaderboardManager.Instance.SubmitScore(GetName(), ScoreManager.Instance.CurrentScore);

        // Make sure only the buttons can be selected now.
        nextSelectable.enabled = true;
        firstCharSelector.enabled = false;

        // Animate
        StartCoroutine(_AnimateToLeaderboard());
    }

    IEnumerator _AnimateToLeaderboard()
    {
        // Basically describes itself.
        m_group.AnimateAlpha(0, animDuration / 2, EaseMode.Step4);
        yield return new WaitForSeconds(animDuration / 2);
        leaderBoardGroup.AnimateAlpha(1, animDuration / 2, EaseMode.Step4);
    }
}
