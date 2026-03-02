using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyWingAnimation : MonoBehaviour
{
    public float minYRotation = -90f;      // Minimum Y rotation angle
    public float maxYRotation = -20f;      // Maximum Y rotation angle
    public float rotationSpeed = 2f;       // Speed of the rotation

    private bool rotatingToMax = true;     // Determines the current direction of rotation
    private float newYRotation;            // Current Y rotation

    private float RotZ;
    void Awake()
    {
        RotZ = transform.rotation.eulerAngles.z;
        RotZ = transform.localRotation.eulerAngles.z;
    }

    void Update()
    {
        // Set the local rotation only on the Y axis
        transform.localRotation = Quaternion.Euler(0, newYRotation, RotZ);

        // Toggle direction when reaching the limits
        if (newYRotation >= maxYRotation)
        {
            rotatingToMax = false;
        }
        else if (newYRotation <= minYRotation)
        {
            rotatingToMax = true;
        }

        // Update newYRotation based on direction and speed
        if (rotatingToMax)
        {
            newYRotation += rotationSpeed * Time.deltaTime;
        }
        else
        {
            newYRotation -= rotationSpeed * Time.deltaTime;
        }
    }
}