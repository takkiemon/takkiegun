using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public int waveNumber;

    public OneWayPattern oneWayScript;
    public GroupPattern groupScript;
    public SinusPattern sinusScript;

    private int currentPattern;

    public MovingWallBehavior[] movingWalls = new MovingWallBehavior[3];
    public MovingWallBehavior[] tempMovingWalls;
    public Vector3[] startingPositions;
    public Vector3[] startingAngles;
    public Vector3[] movementDirections;
    public float movementSpeed;

    private Vector3 defaultWallPosition;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 1;
        defaultWallPosition = new Vector3(16, 0, 10);
        StartPattern(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWalls()
    {
        
    }

    // starts the relevant wave
    public void InitiateWave(int waveNo)
    {

    }

    // should be called after every wave. 
    // gives feedback on the end of the finished wave and also kicks off the next wave
    public void WaveChanger()
    {

    }

    public void StartPattern(int waveNumber) // remove?
    {
        this.waveNumber = waveNumber;
        switch(waveNumber)
        {
            case 0:
                Debug.Log("I don't think there should be a wave 0.");
                break;
            case 1:
                StartWave1();
                break;
            case 2:
                StartWave2();
                break;
            case 3:
                StartWave3();
                break;
            case 4:
                StartWave4();
                break;
            case 5:
                StartWave5();
                break;
            default:
                break;
        }
    }

    #region SetMovementVariales overloads
    public void SetMovementVariables(int firstWall, int lastWall, Vector3[] startingPositions, Vector3[] startingAngles, Vector3[] movementDirections) // TODO: create overload with a speed float instead of float array, so all movingwalls gain the same speed
    {
        int numberOfWalls = lastWall - firstWall;
        if (numberOfWalls != startingPositions.Length || numberOfWalls != startingAngles.Length || numberOfWalls != movementDirections.Length)
        {
            numberOfWalls = Mathf.Min(startingPositions.Length, startingAngles.Length);
            numberOfWalls = Mathf.Min(lastWall - firstWall, numberOfWalls);
            Debug.Log(
                "All these following values should be the same, but that's not the case \n" +
                "startingPositions.Length(" + startingPositions.Length + ")\n" +
                "startingAngles.Length(" + startingAngles.Length + ")\n" +
                "movementDirections.Length(" + movementDirections.Length + ")\n" +
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
            movingWalls[i].transform.eulerAngles = startingAngles[i];
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
            movingWalls[i].transform.eulerAngles = startingAngles;
            //movingWalls[i].movingDirection = movementDirections;
        }
    }
    #endregion

    public void WaveEnded(int waveNumber)
    {
        // some feedback for between waves
        // after the feedback and stuff, start next wave
        StartPattern(waveNumber + 1);
    }

    public void StartWave1() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        int numberOfWallsForWave1 = 4;
        tempMovingWalls = new MovingWallBehavior[numberOfWallsForWave1];
        for (int i = 0; i < numberOfWallsForWave1; i ++)
        {
            tempMovingWalls[i] = movingWalls[i];
        }
        for (int i = 0; i < 4; i++)
        {
            movingWalls[i].guidingHand.SetActive(true);
        }
        oneWayScript.Activate(waveNumber, tempMovingWalls, startingPositions, defaultWallPosition, 20f, 24f, this, movementSpeed);
    }

    public void EndWave1()
    {
        for (int i = 0; i < 4; i++)
        {
            movingWalls[i].guidingHand.SetActive(false);
        }
        oneWayScript.isCurrentPattern = false;
        WaveEnded(waveNumber);
    }

    public void StartWave2() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        int numberOfWallsForWave1 = 12;
        tempMovingWalls = new MovingWallBehavior[numberOfWallsForWave1];
        for (int i = 0; i < numberOfWallsForWave1; i++)
        {
            tempMovingWalls[i] = movingWalls[i];
        }
        oneWayScript.Activate(waveNumber, tempMovingWalls, startingPositions, defaultWallPosition, 20f, 18f, this, movementSpeed);
    }

    public void StartWave3() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        sinusScript.Activate(waveNumber, movingWalls, startingPositions, defaultWallPosition, 20f, 18f, this, movementSpeed);
    }

    public void StartWave4() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        oneWayScript.Activate(waveNumber, movingWalls, startingPositions, defaultWallPosition, 20f, 18f, this, movementSpeed);
    }

    public void StartWave5()
    {
        int numberOfWallsForWave1 = 12;
        tempMovingWalls = new MovingWallBehavior[numberOfWallsForWave1];
        for (int i = 0; i < numberOfWallsForWave1; i++)
        {
            tempMovingWalls[i] = movingWalls[i];
        }
        oneWayScript.Activate(waveNumber, tempMovingWalls, startingPositions, defaultWallPosition, 20f, 18f, this, movementSpeed * 2);
    }

    public void StartUltraWave(int waveNumber)
    {
        groupScript.Activate(waveNumber, movingWalls, startingPositions, defaultWallPosition, 20f, 18f, this, movementSpeed);
        // some cool random advanced behavior with increasing speeds and stuff to sorta make it go endless.
    }
}