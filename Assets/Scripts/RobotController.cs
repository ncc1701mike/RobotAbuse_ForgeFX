using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    //public Transform Torso; 
    //public Transform Arm;
    private Vector3 mouseOffset;
    private float mouseZCoord;

    private void Start()
    {
        //Torso = GameObject.Find("Torso").transform;
        //Arm = GameObject.Find("Arm").transform;
    }

    private void OnMouseDown()  // Get the offset between the mouse position and the object position
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }


    private Vector3 GetMouseWorldPos()  // Get the mouse position in the world
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    private void OnMouseDrag()  // Move the object with the mouse
    {
        transform.position = GetMouseWorldPos() + mouseOffset;
    }

}
