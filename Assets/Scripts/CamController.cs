using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform robotTarget;
    public float moveSpeed = 1.0f;
    public float zoomSpeed = 1.0f;
    public float rotationSpeed = 35.0f;

    void Update()
    {
        Movement();
        ZoomControl();
        RotationControl();
    }

    void Movement()
    {
        // Movement on the X and Y axis
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Space)) direction += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.Space)) direction += Vector3.down;
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.Space)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.Space)) direction += Vector3.right;

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void ZoomControl()
    {
        // Zoom in and out on the Z axis
        if (Input.GetKey(KeyCode.Return)) Zoom(zoomSpeed);
        if (Input.GetKey(KeyCode.RightShift)) Zoom(-zoomSpeed);
    }

    void Zoom(float speed)
    {
        Vector3 zoomDirection = transform.forward * speed * Time.deltaTime;
        transform.Translate(zoomDirection, Space.World);
    }

    void RotationControl()
    {   
        // Rotation around the Robot
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space)) Rotate(Vector3.up, rotationSpeed);
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space)) Rotate(Vector3.up, -rotationSpeed);
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.Space)) Rotate(Vector3.right, rotationSpeed);
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.Space)) Rotate(Vector3.right, -rotationSpeed);
    }

    void Rotate(Vector3 axis, float speed)
    {
        transform.RotateAround(robotTarget.position, axis, speed * Time.deltaTime);
    }
}
