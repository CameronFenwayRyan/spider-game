using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform spider;  // Reference to the Spider GameObject
    public float smoothSpeed = 0.125f;  // Camera follow smoothness
    public float offsetY = 2f;  // Offset on the y-axis to add some space between the spider and camera

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Camera target position, only following the spider's y position
        Vector3 desiredPosition = new Vector3(transform.position.x, spider.position.y + offsetY, transform.position.z);
        
        // Smoothly move the camera toward the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
