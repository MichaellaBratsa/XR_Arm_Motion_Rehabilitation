using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    float rotationSpeed = 0.2f; // Speed of rotation in degrees per second


    static SkyboxRotator Instance; // Singleton instance

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a skybox material is assigned
        if (RenderSettings.skybox != null)
        {
            // Update the "_Rotation" property of the skybox material
            float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");
            currentRotation += rotationSpeed * Time.deltaTime;
            RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
        }
    }
}
