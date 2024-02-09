using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public DetachablePartsConfig partsConfig;

    public void UpdateConfiguration()
    {
        
    }

    // Toggle detachability from the custom editor
    [HideInInspector] public bool detachHead = false;
    [HideInInspector] public bool detachRightArm = false;
    [HideInInspector] public bool detachLeftArm = false;
    [HideInInspector] public bool detachRightLeg = false;
    [HideInInspector] public bool detachLeftLeg = false;
}
