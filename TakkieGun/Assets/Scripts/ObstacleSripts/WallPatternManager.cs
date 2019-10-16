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
    public Vector3[] startingPositions;
    public Vector3[] startingAngles;
    public Vector3[] movementDirections;
    public float[] movementSpeeds;

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

    #region SetMovementVariales overloads
    public void SetMovementVariables(int firstWall, int lastWall, Vector3[] startingPositions, Vector3[] startingAngles, Vector3[] movementDirections, float[] movementSpeeds) // TODO: create overload with a speed float instead of float array, so all movingwalls gain the same speed
    {
        int numberOfWalls = lastWall - firstWall;
        if (numberOfWalls != startingPositions.Length || numberOfWalls != startingAngles.Length || numberOfWalls != movementDirections.Length || numberOfWalls != movementSpeeds.Length)
        {
            numberOfWalls = Mathf.Min(
                Mathf.Min(startingPositions.Length, startingAngles.Length), 
                Mathf.Min(movementDirections.Length, movementSpeeds.Length)
                );
            numberOfWalls = Mathf.Min(lastWall - firstWall, numberOfWalls);
            Debug.Log(
                "All these following values should be the same, but that's not the case \n" +
                "startingPositions.Length(" + startingPositions.Length + ")\n" +
                "startingAngles.Length(" + startingAngles.Length + ")\n" +
                "movementDirections.Length(" + movementDirections.Length + ")\n" +
                "movementSpeeds.Length(" + movementSpeeds.Length + ")\n" +
                "Last Wall - First Wall: (" + lastWall + " - " + firstWall + " = " + (lastWall - firstWall) + ")\n" +
                "The lowest number between these values is " + numberOfWalls + ", so that's the amount of walls that will be used for this operation."
                );
        }

        if (movingWalls.Length != startingPositions.Length)
        {
            if (movingWalls.Length < startingPositions.Length)
            {
                Debug.Log("warning: movingWalls.Length (" + startingPositions.Length + ") < movingWalls.Length (" + startingPositions.Length + "), so SetMovementVariables() will be performed " + movingWalls.Length + " times.");
            }
            else if (movingWalls.Length > startingPositions.Length)
            {
                Debug.Log("warning: movingWalls.Length (" + startingPositions.Length + ") > startingPositions.Length (" + startingPositions.Length + "), so SetMovementVariables() will be performed " + startingPositions.Length + " times.");
            }
        }

        for (int i = 0; i < numberOfWalls; i++)
        {
            movingWalls[i].transform.position = startingPositions[i];
            movingWalls[i].movementSpeed = movementSpeeds[i];
            movingWalls[i].transform.eulerAngles = startingAngles[i];
            movingWalls[i].movingDirection = movementDirections[i];
        }
    }

    public void SetMovementVariables(int numberOfWalls, Vector3[] startingPositions, Vector3 startingAngles, Vector3 movementDirections, float movementSpeeds) // TODO: create overload with a speed float instead of float array, so all movingwalls gain the same speed
    {
        if (numberOfWalls != startingPositions.Length)
        {
            numberOfWalls = Mathf.Min(startingPositions.Length, numberOfWalls);

            Debug.Log(
                "All these following values should be the same, but that's not the case \n" +
                "startingPositions.Length(" + startingPositions.Length + ")\n" +
                "Number of Walls (" + numberOfWalls + ")\n" +
                "The lowest number between these values is " + numberOfWalls + ", so that's the amount of walls that will be used for this operation."
                );
        }

        if (movingWalls.Length != startingPositions.Length)
        {
            if (movingWalls.Length < startingPositions.Length)
            {
                Debug.Log("warning: movingWalls.Length (" + startingPositions.Length + ") < movingWalls.Length (" + startingPositions.Length + "), so SetMovementVariables() will be performed " + movingWalls.Length + " times.");
            }
            else if (movingWalls.Length > startingPositions.Length)
            {
                Debug.Log("warning: movingWalls.Length (" + startingPositions.Length + ") > startingPositions.Length (" + startingPositions.Length + "), so SetMovementVariables() will be performed " + startingPositions.Length + " times.");
            }
        }

        for (int i = 0; i < numberOfWalls; i++)
        {
            movingWalls[i].transform.position = startingPositions[i];
            movingWalls[i].movementSpeed = movementSpeeds;
            movingWalls[i].transform.eulerAngles = startingAngles;
            movingWalls[i].movingDirection = movementDirections;
        }
    }
    #endregion

    public void SingleWallPatternStart() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {

    }
}