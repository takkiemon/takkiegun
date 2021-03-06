﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPattern : MonoBehaviour
{
    public int waveNumber;
    public PatternManager patternManager;
    public MovingWallBehavior[] movingWalls;
    public Vector3 defaultWallPosition = new Vector3(16, 0, 0);
    public Vector3[] startingPositions;
    public Vector3[] startingEulers;

    public float wallVelocity;

    // Start is called before the first frame update
    void Start()
    {
        foreach (MovingWallBehavior wall in movingWalls)
        {
            wall.transform.position = defaultWallPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GetTheWallMoving()
    {
        for (int i = 0; i < movingWalls.Length; i++)
        {
            Rigidbody tempRB = movingWalls[i].GetComponent<Rigidbody>();
            tempRB.velocity = tempRB.transform.forward * wallVelocity;
        }
    }

    public virtual void SetMovementVariables(Vector3 startingPosition, float movementSpeed)
    {
        for (int i = 0; i < movingWalls.Length; i++)
        {
            movingWalls[i].transform.position = new Vector3(defaultWallPosition.x + i * (movingWalls[i].transform.localScale.x + 1), defaultWallPosition.y + (i * movingWalls[i].transform.localScale.y + 1), defaultWallPosition.z);
        }
    }

    public virtual void Activate(int waveNumber, MovingWallBehavior[] movingWalls, Vector3[] startingPositions, Vector3 defaultPosition, PatternManager patternManager, float wallSpeed)
    {
        this.waveNumber = waveNumber;
        this.movingWalls = movingWalls;
        this.startingPositions = startingPositions;
        this.defaultWallPosition = defaultPosition;
        this.patternManager = patternManager;
        this.wallVelocity = wallSpeed;
        WaveStarts();
    }

    public virtual void WaveStarts()
    {

    }

    public virtual void WaveIsDone()
    {
        patternManager.WaveEnded(waveNumber);
    }
}
