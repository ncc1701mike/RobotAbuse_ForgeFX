using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComponentManager : MonoBehaviour
{
    public DetachablePartsConfig partsConfig;
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
        // Initialize component's relative positions
        InitializePartPositions(partsConfig.eyeComponents);
        InitializePartPositions(partsConfig.headComponents);
        InitializePartPositions(partsConfig.torsoComponents);
        InitializePartPositions(partsConfig.rightArmComponents);
        InitializePartPositions(partsConfig.leftArmComponents);
        InitializePartPositions(partsConfig.rightLegComponents);
        InitializePartPositions(partsConfig.leftLegComponents);
    }

    private void InitializePartPositions(List<DetachablePartsConfig.PartConfig> partConfigs)
    {
        // Initialize component's initial positions
        foreach (var partConfig in partConfigs)
        {
            if (partConfig.gameObject != null)
            {
                initialPositionsRelativeToTorso[partConfig.gameObject.transform] = partConfig.gameObject.transform.localPosition;
                partMoved[partConfig.gameObject.transform] = false;
            }
        }
    }

    private void TrySelectPart() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform hitTransform = hit.transform;
            
            if (IsConfiguredPart(hitTransform))
            {
                // Select the component and store its initial position
                selectedPart = hitTransform;
                initialLocalPosition = selectedPart.localPosition;
                mouseZCoord = mainCamera.WorldToScreenPoint(selectedPart.position).z;
                mouseOffset = selectedPart.position - GetMouseWorldPos();

                OnComponentStatusChanged?.Invoke(hit.collider.CompareTag("Torso") ? null : false); 
            }

        }
    }

    private bool IsConfiguredPart(Transform partTransform)
    {
        return partsConfig.IsAnyPart(partTransform);
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
        // Update UI with component status
        OnComponentStatusChanged?.Invoke(isDetached);
    }
}