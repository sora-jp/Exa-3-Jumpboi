using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudibleSelectable : Selectable, ISubmitHandler
{
    public string sfxMove, sfxActivate;
    public bool playMoveSfx = true;

    public override void OnMove(AxisEventData eventData)
    {
        if (playMoveSfx && eventData.moveDir != MoveDirection.None) PlayMoveSfx();
        Debug.Log($"{gameObject.name} mv -> {eventData.moveDir} ({eventData.moveVector})");
        base.OnMove(eventData);
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        AudioManager.PlayEffect(sfxActivate);
    }

    protected void PlayMoveSfx()
    {
        Debug.Log("PMSFX");
        AudioManager.PlayEffect(sfxMove);
    }
}