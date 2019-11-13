using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime;

    public int targetWidth;
    public int targetHeight;

    float targetAr;
    float curAr;

    Vector3 vel;
    Camera cam;
    int lastWidth, lastHeight;

    void Awake()
    {
        cam = GetComponent<Camera>();
        targetAr = (float) targetWidth / targetHeight;
        UpdateCurrentAR();
        UpdateCameraRect();
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Vector3.up * player.position.y + Vector3.back * 10,
            ref vel, smoothTime);

        if (Screen.width != lastWidth || Screen.height != lastHeight) UpdateCurrentAR();
        UpdateCameraRect();
    }

    void UpdateCurrentAR()
    {
        curAr = (float)Screen.width / Screen.height;
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void UpdateCameraRect()
    {
        cam.rect = new Rect(0.5f - (targetAr / curAr) / 2, 0, targetAr / curAr, 1);
    }
}
