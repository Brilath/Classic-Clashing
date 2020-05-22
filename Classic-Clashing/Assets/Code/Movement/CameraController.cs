using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float rotationSpeed = 1f;

    [SerializeField]
    private Camera playerCamera;
    [SerializeField] private float cameraRotation = 0f;
    [SerializeField] private float currentRotation = 0f;

    [SerializeField, Range(-85f, 85f)] private float minCameraRotation = 0f;
    [SerializeField, Range(-85f, 85f)] private float maxCameraRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate Up & Down
        float playerRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        RotateCamera(playerRotation);
    }

    private void FixedUpdate()
    {
        currentRotation -= cameraRotation;
        currentRotation = Mathf.Clamp(currentRotation, minCameraRotation, maxCameraRotation);
        playerCamera.transform.localEulerAngles = new Vector3(currentRotation, 0, 0);
    }

    private void RotateCamera(float rotationVector)
    {
        cameraRotation = rotationVector;
    }
}
