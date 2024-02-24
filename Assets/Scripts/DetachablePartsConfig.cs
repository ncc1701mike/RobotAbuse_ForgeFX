using System.Collections.Generic;
using UnityEngine;

// DetachablePartsConfig - ScriptableObject that stores the detachable parts configuration
[CreateAssetMenu(fileName = "DetachablePartsConfig", menuName = "Robot/DetachablePartsConfig", order = 1)]

public class DetachablePartsConfig : ScriptableObject 
{
    // Lists of detachable part configurations
    public List<PartConfig> headComponents = new List<PartConfig>();
    public List<PartConfig> rightArmComponents = new List<PartConfig>();
    public List<PartConfig> leftArmComponents = new List<PartConfig>();
    public List<PartConfig> rightLegComponents = new List<PartConfig>();
    public List<PartConfig> leftLegComponents = new List<PartConfig>();
    public List<PartConfig> eyeComponents = new List<PartConfig>();
    public List<PartConfig> torsoComponents = new List<PartConfig>(); // Not detachable, but has to be included for highlighting

    
    [System.Serializable]
    public class PartConfig  // Class that stores the part configuration for each detachable component
    {
        public string componentName;
        public string tagName;
        public GameObject gameObject; 
        public MeshRenderer meshRenderer; 
    }
    

    // Function to check if a partTransform is in any of the part lists
    public bool IsAnyPart(Transform partTransform)
    {
        // Combine all components parts lists into one list
        var allParts = new List<PartConfig>();
        allParts.AddRange(headComponents);
        allParts.AddRange(eyeComponents);
        allParts.AddRange(torsoComponents);
        allParts.AddRange(rightArmComponents);
        allParts.AddRange(leftArmComponents);
        allParts.AddRange(rightLegComponents);
        allParts.AddRange(leftLegComponents);
        

        // Check if the partTransform is in the allParts list - if it is, return true, else return false
        foreach (var part in allParts)
        {
            if (part.gameObject.transform == partTransform)
            {
                return true;
            }
        }
        return false;
    }

}
