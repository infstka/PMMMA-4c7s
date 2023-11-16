using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public float turnSpeed = 4.0f; 

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(2)) 
        {
            xRotation -= Input.GetAxis("Mouse Y") * turnSpeed;
            yRotation += Input.GetAxis("Mouse X") * turnSpeed;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }

        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    }
}
