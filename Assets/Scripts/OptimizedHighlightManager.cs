using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizedHighlightManager : MonoBehaviour
{
    [SerializeField] private Renderer[] robotRenderers;  // Array of robot renderers
    public LayerMask highlightLayer;                     // Layer to highlight
    private MaterialPropertyBlock propBlock;             // Material property block - stores material properties for dynamic changes
    private Vector3 lastMousePosition = Vector3.zero;    // Tracks the last known mouse position to optimize raycasting
    private float lastRaycastTime = 0f;                  // Tracks the last time a raycast was performed in order to control raycast frequency
    private float raycastFrequency = 0.1f;               // Defines raycast frequency for performance optimization

    void Start()
    {
        // Initialize renderer array and material property block
        robotRenderers = GetComponentsInChildren<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        // Only raycast if mouse has been moved and sufficient time has passed since the last raycast
        if (Input.mousePosition != lastMousePosition && Time.time - lastRaycastTime >= raycastFrequency)
        {
            PerformRaycast();
            lastMousePosition = Input.mousePosition;
            lastRaycastTime = Time.time;
        }
    }


    // Highlights or resets the entire robot
    public void HighlightRobot(bool highlight)
    {
        SetHighlight(robotRenderers, highlight, false);
    }


    // Highlights or resets a specific detachable part
    public void HighlightDetachablePart(string partHit, bool highlight)
    {
        foreach (Renderer renderer in robotRenderers)
        {
            if (renderer.gameObject.CompareTag(partHit))
            {
                SetHighlight(new Renderer[] { renderer }, highlight, true);
            }
        }
    }

    
    // Resets highlighting to default for all parts
    private void ResetHighlight()
    {
        SetHighlight(robotRenderers, false, false);
    }

    
    // Applies highlighting to the specified renderers based on the highlight and detach highlight flags
    private void SetHighlight(Renderer[] renderers, bool highlight, bool isDetachHighlight)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.GetPropertyBlock(propBlock);

            // Choose color based on highlight or detach highlight
            propBlock.SetColor("_EmissionColor", highlight ? (isDetachHighlight ? Color.red * 0.8f : Color.white * 0.4f) : Color.black);
            renderer.SetPropertyBlock(propBlock);
        }
    }


    // Performs the raycast and determines how to highlight based on the hit object
    private void PerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, highlightLayer))
        {
            // Determine action based on the hit object
            if (hit.collider.CompareTag("Torso"))
            {
                HighlightRobot(true);
            }

            else if (IsDetachablePartTag(hit.collider.tag))
            {
                HighlightDetachablePart(hit.collider.tag, true);
            }

            else
            {
                ResetHighlight();
            }
        }

        else
        {
            ResetHighlight();
        }
    }


    // Checks if the tag belongs to a detachable part
    private bool IsDetachablePartTag(string tag)
    {
        return tag == "Head" || tag == "Right Arm" || tag == "Left Arm" || tag == "Right Leg" || tag == "Left Leg";
    }
}
