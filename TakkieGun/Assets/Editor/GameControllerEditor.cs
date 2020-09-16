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
        if (GUILayout.Button(gameControllerScript.patternNames[0]))
        {
            gameControllerScript.PatternOne();
        }
        if (GUILayout.Button(gameControllerScript.patternNames[1]))
        {
            gameControllerScript.PatternTwo();
        }
        if (GUILayout.Button(gameControllerScript.patternNames[2]))
        {
            gameControllerScript.PatternThree();
        }
        if (GUILayout.Button(gameControllerScript.patternNames[3]))
        {
            gameControllerScript.PatternFour();
        }

        DrawDefaultInspector();

        if (GUILayout.Button("test001"))
        {
            Debug.Log("testing 001");
        }
        if (GUILayout.Button("test002"))
        {
            Debug.Log("testing 002");
        }
        if (GUILayout.Button("test003"))
        {
            Debug.Log("testing 003");
        }
    }
}
