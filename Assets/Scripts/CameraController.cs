using UnityEngine;

/// <summary>
/// Updates the camera rect to match the target ar I designed the game for.
/// </summary>
public class CameraController : MonoBehaviour
{
    public int targetWidth; // Target pixel width
    public int targetHeight; // Target pixel width

    float m_targetAr; // Target aspect ratio (width/height)
    float m_curAr; // Current aspect ratio

    Camera m_cam; // Cached reference to the camera
    int m_lastWidth, m_lastHeight; // The last screen width / height.

    void Awake()
    {
        m_cam = GetComponent<Camera>();
        m_targetAr = (float) targetWidth / targetHeight; // Calculate target aspect

        // Update everything, so everything looks right on the first rendered frame.
        UpdateCurrentAr();
        UpdateCameraRect();
    }

    void Update()
    {
        if (Screen.width != m_lastWidth || Screen.height != m_lastHeight) UpdateCurrentAr(); // Recalculate ar if the screen dimensions change
        UpdateCameraRect(); // Always keep the camera rect up to date.
    }

    void UpdateCurrentAr()
    {
        // Recalculate ar
        m_curAr = (float)Screen.width / Screen.height;
        m_lastWidth = Screen.width;
        m_lastHeight = Screen.height;
    }

    // Recalculate the camera rect.
    void UpdateCameraRect()
    {
        // Found this by trial and error. If i sat with a pen / paper, i could figure out why this works. But for now, it just does :)
        var width = m_targetAr / m_curAr;

        // We want the camera rect in the center. Since the rect is normalized, 0.5 is the halfway point
        // We then subtract half of the width, so that adding width makes the number symmetrical around 0.5
        m_cam.rect = new Rect(0.5f - width / 2, 0, width, 1);
    }
}
