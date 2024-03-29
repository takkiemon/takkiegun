﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternManager : MonoBehaviour
{
    public int levelNumber;
    public int waveNumber;

    public OneWayPattern oneWayScript;
    public SinusPattern sinusScript;

    public MovingWallBehavior[] movingWalls; // this array holds all the existing walls
    public MovingWallBehavior[] tempMovingWalls; // this array holds all the walls that are used for the active wave, so that we can iterate through a whole array
    public float movementSpeed;

    public Text levelText;
    public Text waveText;

    private Vector3 defaultWallPosition;

    public GameObject victoryPanel;
    public AudioSource normalMusic;
    public AudioSource victoryMusic;

    // Start is called before the first frame update
    void Start()
    {
        victoryPanel.SetActive(false);
        defaultWallPosition = new Vector3(16, 0, 10);
        StartPattern(levelNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // should be called after every wave. 
    // gives feedback on the end of the finished wave and also kicks off the next wave
    public void WaveChanger()
    {

    }

    public void StartPattern(int waveNumber) // remove?
    {
        this.levelNumber = waveNumber;
        levelText.text = "Level " + waveNumber + "\nWave 1";
        switch (waveNumber)
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
                levelText.text = "No more waves :( \n You have beaten the game.";
                victoryPanel.SetActive(true);
                normalMusic.Stop();
                victoryMusic.Play();
                break;
            case 5:
                levelText.text = "No more waves :( \n You have beaten the game.";
                break;
            default:
                break;
        }
    }

    #region SetMovementVariables overloads
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
        StopAllWaves();
        StartPattern(waveNumber + 1);
    }

    public void StartWave1() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        Debug.Log("Wave 1 is started.");
        int numberOfWallsForWave1 = 4;
        tempMovingWalls = new MovingWallBehavior[numberOfWallsForWave1];
        for (int i = 0; i < numberOfWallsForWave1; i ++)
        {
            tempMovingWalls[i] = movingWalls[i];
        }
        oneWayScript.waveNumber = 1;
        oneWayScript.levelText = levelText;
        oneWayScript.waveText = waveText;
        oneWayScript.Activate(levelNumber, tempMovingWalls, defaultWallPosition, 20f, 24f, this, movementSpeed);
    }

    public void EndWave1()
    {
        Debug.Log("Wave 1 has ended.");
        oneWayScript.isCurrentPattern = false;
        WaveEnded(levelNumber);
    }

    public void StartWave2() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        Debug.Log("Wave 2 is started.");
        int numberOfWallsForWave1 = 8;
        tempMovingWalls = new MovingWallBehavior[numberOfWallsForWave1];
        for (int i = 0; i < numberOfWallsForWave1; i++)
        {
            tempMovingWalls[i] = movingWalls[i];
        }
        oneWayScript.waveNumber = 1;
        oneWayScript.levelText = levelText;
        oneWayScript.waveText = waveText;
        oneWayScript.Activate(levelNumber, tempMovingWalls, defaultWallPosition, 20f, 18f, this, movementSpeed);
    }

    public void StartWave3() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        Debug.Log("Wave 3 is started.");
        sinusScript.waveType = 1;
        sinusScript.levelText = levelText;
        sinusScript.waveText = waveText;
        sinusScript.Activate(levelNumber, movingWalls, defaultWallPosition, 20f, this);
    }

    public void StartWave4() //create a function that will start waves. I'm not sure yet how I'm going to spread the patterns between the wave types that I have thought of 
    {
        Debug.Log("Wave 4 is started.");
        oneWayScript.levelText = levelText;
        oneWayScript.waveText = waveText;
        oneWayScript.Activate(levelNumber, movingWalls, defaultWallPosition, 20f, 18f, this, movementSpeed);
    }

    public void StartWave5()
    {
        Debug.Log("Wave 5 is started.");
        int numberOfWallsForWave1 = 12;
        tempMovingWalls = new MovingWallBehavior[numberOfWallsForWave1];
        for (int i = 0; i < numberOfWallsForWave1; i++)
        {
            tempMovingWalls[i] = movingWalls[i];
        }
        oneWayScript.levelText = levelText;
        oneWayScript.waveText = waveText;
        oneWayScript.Activate(levelNumber, tempMovingWalls, defaultWallPosition, 20f, 18f, this, movementSpeed * 2);
    }

    public void StartUltraWave(int waveNumber)
    {
        Debug.Log("Wave Ultra is started.");
        sinusScript.Activate(waveNumber, movingWalls, defaultWallPosition, 20f, 18f, this, movementSpeed);
        // some cool random advanced behavior with increasing speeds and stuff to sorta make it go endless.
    }

    public void StopAllWaves()
    {
        oneWayScript.isCurrentPattern = false;
        sinusScript.isCurrentPattern = false;
    }
}