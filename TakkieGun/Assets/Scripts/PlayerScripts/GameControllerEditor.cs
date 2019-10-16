using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameController gameControllerScript = (GameController)target;
        if (GUILayout.Button("Do now"))
        {
            gameControllerScript.SetStuff();
        }
    }
}
