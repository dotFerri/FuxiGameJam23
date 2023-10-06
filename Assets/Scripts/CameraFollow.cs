using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 relativeOffset;
    public float interpolationFactor;
    public bool updateInEditMode;
    // Update is called once per frame
    void LateUpdate()
    {
        if(Application.isPlaying || updateInEditMode)
        {
            // Lerp instantly if in edit mode.
            float lerpAmount = Application.isPlaying ? (Time.deltaTime * interpolationFactor) : 1;

            // Lerp position towards the object
            transform.position = Vector3.Lerp(transform.position, target.position + (target.rotation * relativeOffset), lerpAmount);

            transform.LookAt(target.transform, Vector3.up);
        }
    }
}
