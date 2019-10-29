using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPattern : WallPattern
{
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
        if (isCurrentPattern)
        {
            timePassed += Time.deltaTime;
            if (timePassed > waveDuration)
            {
                WaveIsDone();
            }
        }
    }

    public override void WaveStarts()
    {
        Debug.Log("One Way Pattern started.");
        timePassed = 0;
        waveDuration = (startingDistance + movingWalls.Length * distanceBetweenWalls) / wallVelocity;
        switch (waveNumber)
        {
            case 1:
                StandardPattern();
                break;
            case 2:
                StandardPattern();
                break;
            default:
                break;
        }
    }

    public void StandardPattern() // the pattern that is usually initiated
    {
        startingPositions = new Vector3[] {
            new Vector3(startingDistance, 0, 0),
            new Vector3(0, -startingDistance - distanceBetweenWalls, 0),
            new Vector3(-startingDistance - 2 * distanceBetweenWalls, 0, 0),
            new Vector3(0, startingDistance + 3 * distanceBetweenWalls, 0)
        };

        startingEulers = new Vector3[] {
            new Vector3(0, -90, 0),
            new Vector3(-90, -90, 0),
            new Vector3(180, -90, 0),
            new Vector3(90, -90, 0)
        };

        for (int i = 0; i < movingWalls.Length; i ++)
        {
            /*
            movingWalls[i].transform.position = startingPositions[i];
            movingWalls[i].transform.eulerAngles = startingEulers[i];
            Rigidbody tempRB = movingWalls[i].GetComponent<Rigidbody>();
            tempRB.velocity = wallVelocity * tempRB.transform.forward;
            */
            switch (i % 4)
            {
                case 0:
                    movingWalls[i].Setup(new Vector3(startingDistance + distanceBetweenWalls * i, Random.Range(-3.5f, 3.5f), 0), startingEulers[i % startingEulers.Length], wallVelocity);
                    break;
                case 1:
                    movingWalls[i].Setup(new Vector3(Random.Range(-3.5f, 3.5f), -startingDistance - distanceBetweenWalls * i, 0), startingEulers[i % startingEulers.Length], wallVelocity);
                    break;
                case 2:
                    movingWalls[i].Setup(new Vector3(-startingDistance - distanceBetweenWalls * i, Random.Range(-3.5f, 3.5f), 0), startingEulers[i % startingEulers.Length], wallVelocity);
                    break;
                case 3:
                    movingWalls[i].Setup(new Vector3(Random.Range(-3.5f, 3.5f), startingDistance + distanceBetweenWalls * i, 0), startingEulers[i % startingEulers.Length], wallVelocity);
                    break;
            }
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

    public override void WaveIsDone()
    {
        isCurrentPattern = false;
        if (waveNumber == 1)
        {
            patternManager.EndWave1();
        }
        else
        {
            base.WaveIsDone();
        }
    }
}