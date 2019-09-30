using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GunController))]
public class GunControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GunController gunScript = (GunController)target;
        if(GUILayout.Button("Apply Settings"))
        {
            gunScript.ApplySettings();
            Debug.Log("test002");
        }
    }
}
