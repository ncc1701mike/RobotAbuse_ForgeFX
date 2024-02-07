using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComponentManager : MonoBehaviour
{
    public static event Action<bool?> OnComponentStatusChanged;
    private Camera mainCamera;
    private Transform selectedPart;
    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Vector3 initialLocalPosition;

    // Dictionaries to track moved components and their initial positions relative to the torso
    private Dictionary<Transform, bool> partMoved = new Dictionary<Transform, bool>();
    private Dictionary<Transform, Vector3> initialPositionsRelativeToTorso = new Dictionary<Transform, Vector3>();

    void Awake()
    {
        mainCamera = Camera.main;
        InitializePartsRelativePosition();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectPart();
        }

        if (Input.GetMouseButton(0) && selectedPart != null)
        {
            DragSelectedPart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPart != null && partMoved.ContainsKey(selectedPart))
            {
                if (partMoved[selectedPart])
                {
                    // Snap back only if component was previously marked as moved
                    SnapBackPart();
                    partMoved[selectedPart] = false;
                }
                
                else
                {
                    // Mark component as moved for next click, except for the torso
                    if (!selectedPart.CompareTag("Torso"))
                    {
                        partMoved[selectedPart] = true;
                    }
                }
                selectedPart = null; 
            }
        }
    }

    private void InitializePartsRelativePosition()
    {
        // Initialize the component relative positions
        foreach (Transform part in GetComponentsInChildren<Transform>())
        {
            if (part.CompareTag("Torso") || part.CompareTag("Arm"))
            {
                initialPositionsRelativeToTorso[part] = part.localPosition;
                partMoved[part] = false;
            }
        }
    }

    private void TrySelectPart() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Torso") || hit.collider.CompareTag("Arm"))
            {
                // Select the component and store its initial position
                selectedPart = hit.transform;
                initialLocalPosition = selectedPart.localPosition;
                mouseZCoord = mainCamera.WorldToScreenPoint(selectedPart.position).z;
                mouseOffset = selectedPart.position - GetMouseWorldPos();

                if (hit.collider.CompareTag("Torso"))
                {
                    OnComponentStatusChanged?.Invoke(null);
                }
            }

        }
    }

    private void DragSelectedPart()
    {
        if (selectedPart != null)
        {
            // Drag the selected component
            Vector3 newWorldPosition = GetMouseWorldPos() + mouseOffset;
            selectedPart.position = newWorldPosition;

            if (!selectedPart.CompareTag("Torso"))
            {
                OnComponentStatusChanged?.Invoke(false);
            }
        }
    }

    private void SnapBackPart()
    {
        // Snap back to initial position using the local position
        if (selectedPart != null && initialPositionsRelativeToTorso.ContainsKey(selectedPart))
        {
            OnComponentStatusChanged?.Invoke(true);
            selectedPart.localPosition = initialPositionsRelativeToTorso[selectedPart];
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    public void UpdateComponentStatus(bool? isDetached)
    {
        // Update the UI with the component status
        OnComponentStatusChanged?.Invoke(isDetached);
    }
}