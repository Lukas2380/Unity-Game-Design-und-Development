using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    //public float distance = 8f;
    //public float sensitivityY = 2f;
    //public float minXRotation = -1f;
    //public float maxXRotation = 80f;
    //public LayerMask collisionLayers;
    //public float collisionOffset = 0.01f;

    //private float currentY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Smoothly move the camera to the desired position and rotation using Lerp and Slerp
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.rotation = smoothedRotation;

        // Perform collision detection to prevent camera clipping through obstacles
        //Vector3 dir = transform.position - target.position;
        //Vector3 desiredCameraPosition = target.position + dir.normalized * distance;
        //Vector3 cameraToTarget = desiredCameraPosition - target.position;

        //RaycastHit hit;
        //if (Physics.Raycast(target.position, cameraToTarget, out hit, distance, collisionLayers))
        //{ // Calculate adjusted position to prevent clippingVector3 adjustedPosition = hit.point + hit.normal * collisionOffset;// Smoothly move the camera to the adjusted position using Lerp
          //  transform.position = Vector3.Lerp(transform.position, adjustedPosition, smoothSpeed);
        //}
    }
}
