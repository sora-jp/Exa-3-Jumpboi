using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothTime;
    public float followCutoff;

    Vector3 m_vel;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Vector3.up * Mathf.Max(player.position.y, followCutoff) + Vector3.back * 10,
            ref m_vel, smoothTime);
    }
}
