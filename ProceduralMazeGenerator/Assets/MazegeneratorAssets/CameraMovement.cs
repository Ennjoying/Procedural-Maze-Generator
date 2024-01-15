using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float _cameraSpeed {get; set; }

    private void Start()
    {
        _cameraSpeed = 5f;
    }

    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputHorizontal, 0f, inputVertical);

        // Move the camera in world space
        transform.Translate(movement * _cameraSpeed * 10 * Time.deltaTime, Space.World);
        
        //clamp position to chosen hardcoded values
        //i chose 255 in the x axis and 120 in the z, so the maze is still visible when the camera is zoomed out to the maximum.
        float clampedX = Math.Min(255, Math.Max(transform.position.x, -255));
        float clampedZ = Math.Min(120, Math.Max(transform.position.z, -120));
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}

