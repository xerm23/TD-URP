using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldCanvas : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);

    }
}
