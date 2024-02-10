using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DetachablePartsConfig", menuName = "Robot/DetachablePartsConfig", order = 1)]
public class DetachablePartsConfig : ScriptableObject 
{
    public List<PartConfig> headComponents = new List<PartConfig>();
    public List<PartConfig> rightArmComponents = new List<PartConfig>();
    public List<PartConfig> leftArmComponents = new List<PartConfig>();
    public List<PartConfig> rightLegComponents = new List<PartConfig>();
    public List<PartConfig> leftLegComponents = new List<PartConfig>();
    public List<PartConfig> eyeComponents = new List<PartConfig>();
    public List<PartConfig> torsoComponents = new List<PartConfig>(); // Not detachable, but has to be included for highlighting

    [System.Serializable]
    public class PartConfig 
    {
        public string componentName;
        public string tagName;
        public GameObject gameObject; 
        public MeshRenderer meshRenderer; 
    }

    public bool IsAnyPart(Transform partTransform)
    {
        // Combine parts lists 
        var allParts = new List<PartConfig>();
        allParts.AddRange(headComponents);
        allParts.AddRange(eyeComponents);
        allParts.AddRange(torsoComponents);
        allParts.AddRange(rightArmComponents);
        allParts.AddRange(leftArmComponents);
        allParts.AddRange(rightLegComponents);
        allParts.AddRange(leftLegComponents);
        

        // Check for the partTransform is in the allParts list
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
