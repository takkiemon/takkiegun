using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float easingSpeed = 10f;
    public Vector3 cameraOffset;

    public bool freezeX = false;
    public bool freezeY = false;
    public bool freezeZ = true;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position + cameraOffset;
        Vector3 smoothedPosition = transform.position;
        if (!freezeX)
        {
            smoothedPosition.x = Vector3.Lerp(transform.position, targetPosition, easingSpeed * Time.deltaTime).x;
        }
        if (!freezeY)
        {
            smoothedPosition.y = Vector3.Lerp(transform.position, targetPosition, easingSpeed * Time.deltaTime).y;
        }
        if (!freezeZ)
        {
            smoothedPosition.z = Vector3.Lerp(transform.position, targetPosition, easingSpeed * Time.deltaTime).z;
        }
        transform.position = smoothedPosition;

        //transform.LookAt(playerTransform);
    }
}
