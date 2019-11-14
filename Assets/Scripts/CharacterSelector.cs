using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelector : Selectable, ISubmitHandler
{
    public Image charImg;

    [HideInInspector] public int charIdx = 0;
    VerticalLayoutGroup m_group;
    Sprite[] m_charSprites;
    Image[] m_images;

    protected override void Awake()
    {
        base.Awake();
        m_group = GetComponent<VerticalLayoutGroup>();
        m_charSprites = GetComponentInParent<NameSelector>().characters;
        m_images = GetComponentsInChildren<Image>();

        SetColor(colors.disabledColor);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        SetColor(colors.normalColor);
    }

    void Update()
    {
        if (currentSelectionState == SelectionState.Selected) SetColor(colors.normalColor);
    }

    void SetColor(Color c)
    {
        if (m_images == null) return;
        foreach (var img in m_images) img.color = c;
    }

    public override void OnMove(AxisEventData eventData)
    {
        switch (eventData.moveDir)
        {
            case MoveDirection.Left:
            case MoveDirection.Right:
                return;
            case MoveDirection.None:
                base.OnMove(eventData);
                return;
            case MoveDirection.Up:
                charIdx--;
                break;
            case MoveDirection.Down:
                charIdx++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        while (charIdx < 0) charIdx += m_charSprites.Length;
        while (charIdx >= m_charSprites.Length) charIdx -= m_charSprites.Length;

        charImg.overrideSprite = m_charSprites[charIdx];
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (FindSelectableOnRight() == null)
        {
            GetComponentInParent<NameSelector>().FinishNameInput();
        } 
        else base.OnMove(new AxisEventData(EventSystem.current) {moveDir = MoveDirection.Right, moveVector = Vector2.right});
    }
}
