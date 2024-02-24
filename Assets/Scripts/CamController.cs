using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Reference to the input handler, allowing for flexible input management (e.g., real or mock inputs).
    public IInputHandler inputHandler = new UnityInputHandler();

    // Target transform that the camera will focus on or rotate around.
    public Transform robotTarget;

    // Speed settings for camera movement, zoom, and rotation.
    public float moveSpeed = 1.0f;
    public float zoomSpeed = 1.0f;
    public float rotationSpeed = 35.0f;

    // A test delta time that can be set to override Unity's Time.deltaTime for testing purposes.
    public float testDeltaTime = -1f;

    void Update()
    {
        // Update the camera's position and orientation based on input.
        Movement();
        ZoomControl();
        RotationControl();
    }

    // Handles the camera's movement based on arrow key inputs.
    internal void Movement()
    {
        // Initialize direction vector to zero.
        Vector3 direction = Vector3.zero;

        // Adjust direction based on input, ignoring space bar for movement commands.
        if (inputHandler.GetKey(KeyCode.UpArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.up;
        if (inputHandler.GetKey(KeyCode.DownArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.down;
        if (inputHandler.GetKey(KeyCode.LeftArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.left;
        if (inputHandler.GetKey(KeyCode.RightArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.right;

        // Apply translation to the camera based on calculated direction.
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    // Manages the zoom function of the camera.
    internal void ZoomControl()
    {
        // Zoom in or out based on input keys.
        if (inputHandler.GetKey(KeyCode.Return)) Zoom(zoomSpeed);
        if (inputHandler.GetKey(KeyCode.RightShift)) Zoom(-zoomSpeed);
    }

    // Applies the zoom effect to the camera.
    internal void Zoom(float speed)
    {
        // Calculate zoom direction and apply translation.
        Vector3 zoomDirection = transform.forward * speed * Time.deltaTime;
        transform.Translate(zoomDirection, Space.World);
    }

    // Controls the rotation of the camera around a target based on input.
    internal void RotationControl()
    {
        // Rotate around the target based on arrow keys and space bar combination.
        if (inputHandler.GetKey(KeyCode.RightArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.up, rotationSpeed);
        if (inputHandler.GetKey(KeyCode.LeftArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.up, -rotationSpeed);
        if (inputHandler.GetKey(KeyCode.UpArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.right, rotationSpeed);
        if (inputHandler.GetKey(KeyCode.DownArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.right, -rotationSpeed);
    }

    // Applies rotation to the camera.
    internal void Rotate(Vector3 axis, float speed)
    {
        // Use testDeltaTime if set, otherwise use Unity's deltaTime.
        float deltaTime = testDeltaTime >= 0 ? testDeltaTime : Time.deltaTime;

        // Apply rotation around the specified axis at the given speed.
        transform.RotateAround(robotTarget.position, axis, speed * deltaTime);
    }
}
