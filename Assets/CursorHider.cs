using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHider : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
#endif
}
