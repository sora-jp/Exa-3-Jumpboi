using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AudibleButton : AudibleSelectable
{
    public UnityEvent onClick;

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        onClick?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        base.OnSubmit(eventData);
    }
}
