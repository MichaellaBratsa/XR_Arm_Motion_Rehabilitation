using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // Required for the UI Image component
using Google.XR.Cardboard;

public class VRButton : MonoBehaviour
{
    [Header("Event to Trigger")]
    // The function you want to run (configured in the Inspector)
    public UnityEvent OnClick;

    [Header("Raycast Settings")]
    public float maxDistance = 10f;

    [Header("Gaze (Dwell) Settings")]
    [Tooltip("How many seconds the player must look at the object to click it.")]
    public float gazeTimeToClick = 2.0f;

    [Tooltip("Reference to the UI Image used as a loading ring.")]
    public Image loadingRing;

    private float timer = 0f;
    private bool isBeingLookedAt = false;
    private bool hasClicked = false;

    void Start()
    {
        // Ensure the loading ring starts empty
        if (loadingRing != null)
        {
            loadingRing.fillAmount = 0f;
        }
    }

    void Update()
    {
        // Create a ray from the center of the camera forward
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if the ray hits SOMETHING and if that something is THIS object
        bool isHit = Physics.Raycast(ray, out hit, maxDistance) && hit.transform == this.transform;

        if (isHit)
        {
            // If the player just started looking at it (first frame)
            if (!isBeingLookedAt)
            {
                isBeingLookedAt = true;
                timer = 0f;
                hasClicked = false;
                OnPointerEnter();
            }

            // Allow immediate click via screen touch or Cardboard trigger
            if (!hasClicked && (Api.IsTriggerPressed || Input.GetMouseButtonDown(0)))
            {
                TriggerClick();
            }

            // Increase the timer if it hasn't been clicked yet
            if (!hasClicked)
            {
                timer += Time.deltaTime;

                // Update the visual loading ring fill amount
                if (loadingRing != null)
                {
                    loadingRing.fillAmount = timer / gazeTimeToClick;
                }

                // If the time exceeds our limit
                if (timer >= gazeTimeToClick)
                {
                    TriggerClick();
                }
            }
        }
        else
        {
            // If the player looks away from the object, reset everything
            if (isBeingLookedAt)
            {
                ResetGaze();
            }
        }
    }

    void TriggerClick()
    {
        OnClick.Invoke();
        hasClicked = true;

        // Reset the ring immediately after clicking
        if (loadingRing != null)
        {
            loadingRing.fillAmount = 0f;
        }
    }

    void ResetGaze()
    {
        isBeingLookedAt = false;
        timer = 0f;
        hasClicked = false;

        // Empty the loading ring when looking away
        if (loadingRing != null)
        {
            loadingRing.fillAmount = 0f;
        }

        OnPointerExit();
    }

    // Visual feedback when looking at the object
    public void OnPointerEnter()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Reset visual feedback when looking away
    public void OnPointerExit()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}