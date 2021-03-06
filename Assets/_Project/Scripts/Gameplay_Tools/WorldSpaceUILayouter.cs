using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUILayouter : MonoBehaviour
{
    private void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
        transform.Rotate(0, 180, 0);
    }
}
