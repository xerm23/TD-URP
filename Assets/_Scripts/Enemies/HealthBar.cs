using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script is used to rotate canvas towards the camera
/// this way HP bar can be seen the same no matter the angle between cam and enemy
/// </summary>
public class HealthBar : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward, Vector3.up);
    }
}
