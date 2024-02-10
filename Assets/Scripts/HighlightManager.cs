using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public Material highlightMat;
    public Material detachHighlightMat;
    public Material defaultMat;
    public LayerMask highlightLayer;
    public DetachablePartsConfig partsConfig;

    
    private void Update()
    {
        // Check if mouse is over a robot part
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the ray hits a collider, check the tag and highlight the corresponding group
        if (Physics.Raycast (ray, out hit, Mathf.Infinity, highlightLayer))
        {
           switch (hit.collider.tag)
           {
               case "Torso":
                    HighlightAllParts(highlightMat);
                    break;

                case "Right Arm":
                    HighlightGroup(partsConfig.rightArmComponents.ConvertAll(r => r.meshRenderer).ToArray(), detachHighlightMat);
                    HighlightDetachableParts(defaultMat, "Right Arm"); // Reset other parts to default except right arm
                    break;

                case "Left Arm":
                    HighlightGroup(partsConfig.leftArmComponents.ConvertAll(r => r.meshRenderer).ToArray(), detachHighlightMat);
                    HighlightDetachableParts(defaultMat, "Left Arm"); // Reset other parts to default except left arm
                    break;

                case "Right Leg":
                    HighlightGroup(partsConfig.rightLegComponents.ConvertAll(r => r.meshRenderer).ToArray(), detachHighlightMat);
                    HighlightDetachableParts(defaultMat, "Right Leg"); // Reset other parts to default except right leg
                    break;

                case "Left Leg":
                    HighlightGroup(partsConfig.leftLegComponents.ConvertAll(r => r.meshRenderer).ToArray(), detachHighlightMat);
                    HighlightDetachableParts(defaultMat, "Left Leg"); // Reset other parts to default except left leg
                    break;

                case "Head":
                    HighlightGroup(partsConfig.headComponents.ConvertAll(r => r.meshRenderer).ToArray(), detachHighlightMat);
                    HighlightDetachableParts(defaultMat, "Head"); // Reset other parts to default except head
                    break;

                
                default:
                    ResetHighlights();
                    break;
           }
        }

        else
        {
            HighlightAllParts(defaultMat);
            ResetHighlights();
        }
    }

    private void HighlightGroup(Renderer[] renderers, Material mat)
    {
        // Highlight or reset specific components
        foreach (Renderer renderer in renderers)
        {
            renderer.material = mat;
        }
    }

    private void HighlightAllParts(Material mat)
    {
        // Highlight or reset all components
        HighlightGroup(partsConfig.headComponents.ConvertAll(r => r.meshRenderer).ToArray(), mat);
        HighlightGroup(partsConfig.torsoComponents.ConvertAll(r => r.meshRenderer).ToArray(), mat);
        HighlightGroup(partsConfig.rightArmComponents.ConvertAll(r => r.meshRenderer).ToArray(), mat);
        HighlightGroup(partsConfig.leftArmComponents.ConvertAll(r => r.meshRenderer).ToArray(), mat);
        HighlightGroup(partsConfig.rightLegComponents.ConvertAll(r => r.meshRenderer).ToArray(), mat);
        HighlightGroup(partsConfig.leftLegComponents.ConvertAll(r => r.meshRenderer).ToArray(), mat);
    }
    
    private void HighlightDetachableParts(Material defaultMaterial, string excludeTag = "")
    {
        // Highlight or reset components based on exclude Tag
        if (excludeTag != "Right Arm")
            HighlightGroup(partsConfig.rightArmComponents.ConvertAll(r => r.meshRenderer).ToArray(), defaultMaterial);
        if (excludeTag != "Left Arm")
            HighlightGroup(partsConfig.leftArmComponents.ConvertAll(r => r.meshRenderer).ToArray(), defaultMaterial);
        if (excludeTag != "Right Leg")
            HighlightGroup(partsConfig.rightLegComponents.ConvertAll(r => r.meshRenderer).ToArray(), defaultMaterial);
        if (excludeTag != "Left Leg")
            HighlightGroup(partsConfig.leftLegComponents.ConvertAll(r => r.meshRenderer).ToArray(), defaultMaterial);
        if (excludeTag != "Head")
            HighlightGroup(partsConfig.headComponents.ConvertAll(r => r.meshRenderer).ToArray(), defaultMaterial);
    }

    private void ResetHighlights()
    {
        // Reset all components to default mat
        HighlightGroup(partsConfig.torsoComponents.ConvertAll(r => r.meshRenderer).ToArray(), defaultMat);
        HighlightDetachableParts(defaultMat); 
    }
}
