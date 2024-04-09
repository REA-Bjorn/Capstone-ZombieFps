using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RotTree))]
public class RotTreeInspector : Editor
{
    public override void OnInspectorGUI()
    {
#if UNITY_EDITOR
        DrawDefaultInspector();
        RotTree _script = target as RotTree;
        if (GUILayout.Button("Randomize Tree Rotations"))
        {
            _script.InspectorButton();
        }
#endif
    }
}
