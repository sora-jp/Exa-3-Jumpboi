using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Allows the user to select a single character, as a part of their name.
/// The current character is shown as an image, and on submit the character selector shifts to the next one in the cycle
/// </summary>
public class CharacterSelector : AudibleSelectable
{
    public Image charImg; // Character image (where we show the current character)

    [HideInInspector] public int charIdx; // Current character index (0 = 'A', 25 = 'Z')
    Sprite[] m_charSprites; // Sprites corresponding to each character index (0 = picture of A, 25 = picture of Z)
    Image[] m_images; // All image components making this selector up (for changing the color of the entire selector at once)

    protected override void Awake()
    {
        base.Awake();
        m_charSprites = GetComponentInParent<NameSelector>().characters;
        m_images = GetComponentsInChildren<Image>();

        // Default the color to disabled (dark).
        SetColor(colors.disabledColor);
    }

    void Update()
    {
        // If we are selected, we should have the normal color (white). If we are not selected, keep our last color
        if (currentSelectionState == SelectionState.Selected) SetColor(colors.normalColor);
    }

    // Sets the color of all sub-images of the selector to c
    // Pretty self-explanatory
    void SetColor(Color c)
    {
        if (m_images == null) return;
        foreach (var img in m_images) img.color = c;
    }

    public override void OnMove(AxisEventData eventData)
    {
        switch (eventData.moveDir)
        {
            default:
                return;
            case MoveDirection.None:
                base.OnMove(eventData);
                return;
            case MoveDirection.Up: // Up goes C -> B -> A
                charIdx--;
                break;
            case MoveDirection.Down: // Down goes A -> B -> C
                charIdx++;
                break;
        }
        PlayMoveSfx();

        // Wrap the charIdx at alphabet boundaries (Y -> Z -> A -> B, and same back)
        while (charIdx < 0) charIdx += m_charSprites.Length;
        while (charIdx >= m_charSprites.Length) charIdx -= m_charSprites.Length;

        // Show the current character.
        charImg.overrideSprite = m_charSprites[charIdx];
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData); // Always call through to base (in this case plays a sound effect)

        // If there is nothing to the right, this is the last selector. Tell the name manager to collect the name and save the score.
        // Otherwise, move to the right (next selector)
        if (FindSelectableOnRight() == null)
        {
            GetComponentInParent<NameSelector>().FinishNameInput();
        } 
        else base.OnMove(new AxisEventData(EventSystem.current) {moveDir = MoveDirection.Right, moveVector = Vector2.right});
    }
}
