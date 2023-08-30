using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Object that the camera following")]
    public Transform TargetObject;

    [Tooltip("Camera")]
    public Transform CamTransform;

    [Tooltip("Offset between camera and following object ")]
    public Vector3 offSet;

    [Range(0.0f, 1.0f)]
    public float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        offSet = CamTransform.position - TargetObject.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = TargetObject.position + offSet;

        CamTransform.position = Vector3.SmoothDamp(CamTransform.position, newPos, ref velocity, SmoothTime);
        transform.LookAt(TargetObject);
    }
}
