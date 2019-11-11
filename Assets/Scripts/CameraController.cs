using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime;
    Vector3 vel;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Vector3.up * player.position.y + Vector3.back * 10,
            ref vel, smoothTime);
    }
}
