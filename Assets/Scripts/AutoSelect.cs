using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelect : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Selectable>().Select();

        // Needed because of a bug in unity, explanation: http://answers.unity.com/answers/1567284/view.html
        GetComponent<Selectable>().OnSelect(null);
    }
}
