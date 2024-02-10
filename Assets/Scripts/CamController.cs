using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Input handler Interface for input handling to facilitate testing
    public IInputHandler inputHandler = new UnityInputHandler();
    public Transform robotTarget;
    public float moveSpeed = 1.0f;
    public float zoomSpeed = 1.0f;
    public float rotationSpeed = 35.0f;
    public float testDeltaTime = -1f;


    void Update()
    {
        Movement();
        ZoomControl();
        RotationControl();
    }


    internal void Movement()
    {
        // Movement on the X and Y axis
        Vector3 direction = Vector3.zero;
        if (inputHandler.GetKey(KeyCode.UpArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.up;
        if (inputHandler.GetKey(KeyCode.DownArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.down;
        if (inputHandler.GetKey(KeyCode.LeftArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.left;
        if (inputHandler.GetKey(KeyCode.RightArrow) && !inputHandler.GetKey(KeyCode.Space)) direction += Vector3.right;

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }


    internal void ZoomControl()
    {
        // Zoom in and out on the Z axis
        if (inputHandler.GetKey(KeyCode.Return)) Zoom(zoomSpeed);
        if (inputHandler.GetKey(KeyCode.RightShift)) Zoom(-zoomSpeed);
    }


    internal void Zoom(float speed)
    {
        Vector3 zoomDirection = transform.forward * speed * Time.deltaTime;
        transform.Translate(zoomDirection, Space.World);
    }


    internal void RotationControl()
    {   
        // Rotation around the Robot
        if (inputHandler.GetKey(KeyCode.RightArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.up, rotationSpeed);
        if (inputHandler.GetKey(KeyCode.LeftArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.up, -rotationSpeed);
        if (inputHandler.GetKey(KeyCode.UpArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.right, rotationSpeed);
        if (inputHandler.GetKey(KeyCode.DownArrow) && inputHandler.GetKey(KeyCode.Space)) Rotate(Vector3.right, -rotationSpeed);
    }


    internal void Rotate(Vector3 axis, float speed)
    {
        float deltaTime = testDeltaTime >= 0 ? testDeltaTime : Time.deltaTime;
        Quaternion initialRotation = transform.rotation;
        transform.RotateAround(robotTarget.position, axis, speed * deltaTime);
    }
}
