using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudibleSelectable : Selectable, ISubmitHandler
{
    public string sfxMove, sfxActivate;
    public bool playMoveSfx = true;

    public override void OnMove(AxisEventData eventData)
    {
        if (playMoveSfx) PlayMoveSfx();
        base.OnMove(eventData);
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        AudioManager.PlayEffect(sfxActivate);
    }

    protected void PlayMoveSfx() => AudioManager.PlayEffect(sfxMove);
}