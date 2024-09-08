using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristLookDetector : MonoBehaviour
{
    public Transform headTransform; // Reference to the VR headset (head)
    public Transform wristTransform; // Reference to the wrist (hand) object
    public GameObject objectToEnable; // The object to enable when looking at wrist
    public GameObject otherPanelObject;
    
    public float maxLookAngle = 30f; // Maximum angle between head and wrist for detection
    public float maxDistance = 0.5f; // Maximum distance between head and wrist for detection

    private bool isLookingAtWrist;

    void Update()
    {
        CheckIfLookingAtWrist();
    }

    // Check if the player is looking at their wrist
    void CheckIfLookingAtWrist()
    {
        // Calculate the direction from the head to the wrist
        Vector3 directionToWrist = wristTransform.position - headTransform.position;

        // Get the forward direction of the head (where the player is looking)
        Vector3 headForward = headTransform.forward;

        // Check the angle between the head's forward direction and the wrist's position
        float angle = Vector3.Angle(headForward, directionToWrist);

        // Check the distance between the head and the wrist
        float distance = Vector3.Distance(headTransform.position, wristTransform.position);

        // Determine if the player is looking at their wrist (within angle and distance)
        if (angle <= maxLookAngle && distance <= maxDistance)
        {
            if (!isLookingAtWrist)
            {
                isLookingAtWrist = true;
                EnableObject(true); // Enable the GameObject when looking at the wrist
            }
        }
        else
        {
            if (isLookingAtWrist)
            {
                isLookingAtWrist = false;
                EnableObject(false); // Disable the GameObject when not looking at the wrist
            }
        }
    }

    // Enable or disable the object based on the player's wrist view
    void EnableObject(bool enable)
    {
        if (objectToEnable != null && !otherPanelObject.activeSelf && GameManager.instance.GetGameActive)
        {
            objectToEnable.SetActive(enable);
        }
    }
}