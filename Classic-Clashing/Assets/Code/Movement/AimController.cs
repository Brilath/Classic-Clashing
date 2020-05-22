using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float rotationSpeed = 1f;

    [SerializeField]
    private GameObject weapon;

    [SerializeField] private Vector3 weaponBaseRotation = new Vector3(180, -90, -90);
    [SerializeField] private float aimRotation = 0f;
    [SerializeField] private float currentRotation = 0f;

    [SerializeField, Range(-85f, 85f)] private float minAimRotation = 0f;
    [SerializeField, Range(-85f, 85f)] private float maxAimRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate Up & Down
        float playerRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        RotateCamera(playerRotation);
    }

    private void FixedUpdate()
    {
        currentRotation -= aimRotation;
        currentRotation = Mathf.Clamp(currentRotation, minAimRotation, maxAimRotation);
        weapon.transform.localEulerAngles = new Vector3(0, currentRotation, 0) + weaponBaseRotation;
    }

    private void RotateCamera(float rotationVector)
    {
        aimRotation = rotationVector;
    }
}
