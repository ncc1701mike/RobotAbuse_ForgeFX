using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SelectionManager))]
public class SelectionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector options
        base.OnInspectorGUI();

        SelectionManager selectionManager = (SelectionManager)target;

        // Custom GUI elements for detachable part selection
        EditorGUILayout.LabelField("Select Components to Detach", EditorStyles.boldLabel);

        selectionManager.detachHead = EditorGUILayout.Toggle("Head", selectionManager.detachHead);
        selectionManager.detachRightArm = EditorGUILayout.Toggle("Right Arm", selectionManager.detachRightArm);
        selectionManager.detachLeftArm = EditorGUILayout.Toggle("Left Arm", selectionManager.detachLeftArm);
        selectionManager.detachRightLeg = EditorGUILayout.Toggle("Right Leg", selectionManager.detachRightLeg);
        selectionManager.detachLeftLeg = EditorGUILayout.Toggle("Left Leg", selectionManager.detachLeftLeg);

        // Update managers when changes are made
        if (GUI.changed)
        {
            // I don't know what kind of methods should be called here to update this
            EditorUtility.SetDirty(selectionManager);
        }
    }
}
