using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPattern : WallPattern
{
    public float delayBetweenWalls;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < movingWalls.Length; i++)
        {
            movingWalls[i].transform.position = new Vector3(defaultWallPosition.x + i * (movingWalls[i].transform.localScale.x + 1), defaultWallPosition.y + (i * movingWalls[i].transform.localScale.y + 1), defaultWallPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void WaveStarts()
    {
        switch (waveNumber)
        {
            case 1:
                StandardPattern();
                break;
            default:
                break;
        }
    }

    public void StandardPattern() // the pattern that is usually initiated
    {
        startingPositions = new Vector3[] {
            new Vector3(12, 0, 0),
            new Vector3(0, -12 - delayBetweenWalls, 0),
            new Vector3(-12 - 2 * delayBetweenWalls, 0, 0),
            new Vector3(0, 12 + 3 * delayBetweenWalls, 0)
        };

        startingEulers = new Vector3[] {
            new Vector3(0, -90, 0),
            new Vector3(-90, -90, 0),
            new Vector3(180, -90, 0),
            new Vector3(90, -90, 0)
        };

        for (int i = 0; i < movingWalls.Length; i ++)
        {
            movingWalls[i].transform.position = startingPositions[i];
            movingWalls[i].transform.eulerAngles = startingEulers[i];
            Rigidbody tempRB = movingWalls[i].GetComponent<Rigidbody>();
            tempRB.velocity = wallVelocity * tempRB.transform.forward;
        }
    }

    public void RandomPattern() // a full pattern with random variables and order of directions
    {

    }

    public void RandomOneShot() // a single wall with random variables
    {

    }

    public void OneShot(int wallNumber, Vector3 startingPosition, Vector3 startingAngle, float speed) // a single wall 
    {
        movingWalls[wallNumber].Setup(startingPosition, startingAngle, speed);
    }

    public override void GetTheWallMoving()
    {
        /*
        foreach (MovingWallBehavior wall in movingWalls)
        {
            wall.GetComponent<Rigidbody>().velocity = wallVelocity;
        }
        */
    }

    public override void SetMovementVariables(Vector3 startingPosition, float movementSpeed)
    {

    }
}