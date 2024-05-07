using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomizeRocks))]
public class CustomizeRocksInspector : Editor
{
    public override void OnInspectorGUI()
    {
#if UNITY_EDITOR
        DrawDefaultInspector();
        CustomizeRocks _script = target as CustomizeRocks;
        if (GUILayout.Button("Randomize Rocks"))
        {
            _script.InspectorButton();
        }
#endif
    }
}
