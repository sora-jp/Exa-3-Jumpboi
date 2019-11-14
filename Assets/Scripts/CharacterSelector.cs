using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelector : Selectable, ISubmitHandler
{
    public Image charImg;

    public int charIdx = 0;
    Sprite[] charSprites;

    protected override void Awake()
    {
        base.Awake();
        charSprites = GetComponentInParent<NameSelector>().characters;

        SetColor(colors.disabledColor);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        SetColor(colors.normalColor);
    }

    void SetColor(Color c)
    {
        foreach (var img in GetComponentsInChildren<Image>()) img.color = c;
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

        while (charIdx < 0) charIdx += charSprites.Length;
        while (charIdx >= charSprites.Length) charIdx -= charSprites.Length;

        charImg.overrideSprite = charSprites[charIdx];
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
