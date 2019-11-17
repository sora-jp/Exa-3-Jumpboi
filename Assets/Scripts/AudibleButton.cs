using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// A button which plays a sound on move / click.
/// </summary>
public class AudibleButton : AudibleSelectable
{
    public UnityEvent onClick;

    public override void OnSubmit(BaseEventData eventData) // Called when the button is selected and enter is pressed.
    {
        base.OnSubmit(eventData);
        onClick?.Invoke(); // Call the onClick callback
    }

    public override void OnPointerUp(PointerEventData eventData) // Called after a click.
    {
        // We treat OnPointerUp and OnSubmit as the same, since they both should call the onClick action, and play the click sound.
        base.OnPointerUp(eventData);
        OnSubmit(eventData);
    }
}
