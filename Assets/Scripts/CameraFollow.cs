using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{    
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            new Vector3(0, transform.position.y, -10),
            new Vector3(0, target.position.y + 2.5f, -10),
            ref velocity, smoothTime);
    }
}
