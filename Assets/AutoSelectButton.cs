using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelectButton : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Button>().Select();
    }
}
