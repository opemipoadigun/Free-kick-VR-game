using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPointController : MonoBehaviour
{
    public Transform playerController; // Reference to the OVRPlayerController
    public float distance = 15f; // Distance in front of the player
    public float height = 3.24f; // Height of the shooting point

    void Update()
    {
        // Calculate the position of the shooting point
        Vector3 forwardDirection = playerController.forward;
        Vector3 shootingPointPosition = playerController.position + forwardDirection * distance;
        shootingPointPosition.y = height; // Adjust the height

        // Set the position of the shooting point
        transform.position = shootingPointPosition;
    }
}
