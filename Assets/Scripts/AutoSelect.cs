using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Automatically select a Selectable on enable, and when nothing else is selected
/// </summary>
public class AutoSelect : MonoBehaviour
{
    void OnEnable()
    {
        // Select as soon as we are enabled
        SelectThis();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // No active selection, reset selection to this.
            SelectThis();
        }
    }

    // Select this
    void SelectThis()
    {
        // Selectable select does everything we need.
        GetComponent<Selectable>().Select();

        // Needed because of a bug in unity, explanation: http://answers.unity.com/answers/1567284/view.html
        // Basically, the selectable is correctly selected, but the graphic is not updated. So while it doesn't look selected, it actually is.
        // This forces the graphic to update.
        GetComponent<Selectable>().OnSelect(null);
    }
}
