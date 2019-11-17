using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A selectable which makes a noise on move / submit
/// </summary>
public class AudibleSelectable : Selectable, ISubmitHandler
{
    public string sfxMove; // Sfx for moving the selection (arrow keys)
    public string sfxActivate; // Sfx for submit event (user presses submit key while this is selected)
    public bool playMoveSfx = true; // Should we play the move sfx?

    public override void OnMove(AxisEventData eventData)
    {
        if (playMoveSfx && eventData.moveDir != MoveDirection.None) PlayMoveSfx(); // Only play the move sound if we actually moved.
        base.OnMove(eventData); // IMPORTANT TO CALL BASE
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        AudioManager.PlayEffect(sfxActivate);
    }

    protected void PlayMoveSfx()
    {
        AudioManager.PlayEffect(sfxMove);
    }
}