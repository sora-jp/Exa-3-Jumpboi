using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelect : MonoBehaviour
{
    void OnEnable()
    {
        SelectThis();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectThis();
        }
    }

    void SelectThis()
    {
        GetComponent<Selectable>().Select();

        // Needed because of a bug in unity, explanation: http://answers.unity.com/answers/1567284/view.html
        GetComponent<Selectable>().OnSelect(null);
    }
}
