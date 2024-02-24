using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializer : MonoBehaviour
{
    
    // Reference to the DetachablePartsConfig scriptable object
    public DetachablePartsConfig partsConfig;

    void Awake()
    {
        // Initialize the components in each detachable part group from the DetachablePartsConfig scriptable object
        InitializeComponents(partsConfig.headComponents);

        InitializeComponents(partsConfig.eyeComponents);

        InitializeComponents(partsConfig.torsoComponents);

        InitializeComponents(partsConfig.rightArmComponents);

        InitializeComponents(partsConfig.leftArmComponents);

        InitializeComponents(partsConfig.rightLegComponents);

        InitializeComponents(partsConfig.leftLegComponents);
    }



    // Function to initialize the components in each detachable part group
    private void InitializeComponents(List<DetachablePartsConfig.PartConfig> partConfigs)
    {
        //Initialize the data from each detachable part group in the DetachablePartsConfig scriptable object
        foreach (var partConfig in partConfigs)
        {
            // Find the GameObject in the scene with the same name as the componentName
            GameObject sceneObject = GameObject.Find(partConfig.componentName);
            if (sceneObject != null)
            {
                // Set the gameObject and meshRenderer properties of the partConfig to the sceneObject and its MeshRenderer component
                partConfig.gameObject = sceneObject;
                partConfig.meshRenderer = sceneObject.GetComponent<MeshRenderer>();
            }
        }
    }
}
