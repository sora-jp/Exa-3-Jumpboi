using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int targetWidth;
    public int targetHeight;

    float m_targetAr;
    float m_curAr;

    Camera m_cam;
    int m_lastWidth, m_lastHeight;

    void Awake()
    {
        m_cam = GetComponent<Camera>();
        m_targetAr = (float) targetWidth / targetHeight;
        UpdateCurrentAr();
        UpdateCameraRect();
    }

    void Update()
    {
        if (Screen.width != m_lastWidth || Screen.height != m_lastHeight) UpdateCurrentAr();
        UpdateCameraRect();
    }

    void UpdateCurrentAr()
    {
        m_curAr = (float)Screen.width / Screen.height;
        m_lastWidth = Screen.width;
        m_lastHeight = Screen.height;
    }

    void UpdateCameraRect()
    {
        m_cam.rect = new Rect(0.5f - (m_targetAr / m_curAr) / 2, 0, m_targetAr / m_curAr, 1);
    }
}
