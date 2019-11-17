using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hides the cursor on game start
/// </summary>
public class CursorHider : MonoBehaviour
{
    void Awake()
    {
        // Hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // If we are in the editor, and we press the escape key, unlock the cursor (so we don't get stuck)
#if UNITY_EDITOR
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
#endif
}
