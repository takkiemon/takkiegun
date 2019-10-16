using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPatternManager : MonoBehaviour
{
    public OneWayPattern oneWayScript;
    public DoublePattern doubleScript;
    public GroupPattern groupScript;

    private int currentPattern;

    public MovingWallBehavior[] movingWalls = new MovingWallBehavior[3];
    private Vector3 defaultWallPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartPattern(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWalls()
    {
        
    }

    public void StartPattern(int patternNumber)
    {
        currentPattern = patternNumber;
        switch(patternNumber)
        {
            case 0:
                oneWayScript.enabled = false;
                doubleScript.enabled = false;
                groupScript.enabled = false;
                break;
            case 1:
                oneWayScript.enabled = true;
                doubleScript.enabled = false;
                groupScript.enabled = false;
                oneWayScript.GetTheWallMovin();
                break;
            case 2:
                oneWayScript.enabled = false;
                doubleScript.enabled = true;
                groupScript.enabled = false;
                break;
            case 3:
                oneWayScript.enabled = false;
                doubleScript.enabled = false;
                groupScript.enabled = true;
                break;
            default:
                break;
        }
    }

    public void SetMovementVariables(Vector3[] startingPositions, float movementSpeed, int numberOfWalls)
    {
        if (startingPositions.Length > numberOfWalls)
        {
            Debug.Log("error: startingPositions.Length (" + startingPositions.Length +  ") > numberOfWalls (" + numberOfWalls + ". SetMovementVariables() aborted.");
            return;
        } 
        else if (startingPositions.Length < numberOfWalls)
        {
            Debug.Log("warning: startingPositions.Length (" + startingPositions.Length + ") < numberOfWalls (" + numberOfWalls + ", so numberOfWalls is set to " + startingPositions.Length + ". SetMovementVariables() will proceed accordingly.");
            numberOfWalls = startingPositions.Length;
        }


    }
}
