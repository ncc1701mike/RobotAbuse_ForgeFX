using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component to display the component status
    public TextMeshProUGUI statusText; 


    private void Start()
    {
        // Initialize the status text on UI start.
        statusText.text = "Component Status:";
    }


    // Updates the UI to reflect the current component attachment status.
    public void UpdateComponentStatus(bool? isAttached)
    {
        if (isAttached.HasValue)
        {
            // Update the status text color and message based on whether the component is attached.
            if (isAttached.Value)
            {
                statusText.text = "Component Status: <color=#00FF00>Attached</color>";
            }

            else
            {
                statusText.text = "Component Status: <color=#FF0000>Detached</color>";
            }
        }

        else
        {
            // Reset to the default status message when no component is selected.
            statusText.text = "Component Status:";
        }
    }

    private void OnEnable()
    {
        // Subscribe to the OnComponentStatusChanged event to receive component status updates.
        ComponentManager.OnComponentStatusChanged += UpdateComponentStatus;
        OptimizedComponentManager.OnComponentStatusChanged += UpdateComponentStatus;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnComponentStatusChanged event when the component is disabled.
        ComponentManager.OnComponentStatusChanged -= UpdateComponentStatus;
        OptimizedComponentManager.OnComponentStatusChanged -= UpdateComponentStatus;
    }
}
