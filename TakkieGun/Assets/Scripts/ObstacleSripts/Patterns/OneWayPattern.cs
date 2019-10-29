using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPattern : WallPattern
{
    public int waveType;

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
        Debug.Log("One Way Pattern started. stats: waveNo: " + waveNumber + ", wavetype: " + waveType);
        timePassed = 0;
        switch (waveNumber)
        {
            case 1:
                waveDuration = (startingDistance + movingWalls.Length * distanceBetweenWalls) / wallVelocity;
                if (waveType == 2)
                {
                    wallVelocity = 10f;
                    startingDistance = 24f;
                    waveDuration = (startingDistance + movingWalls.Length * distanceBetweenWalls) / wallVelocity + 1;
                    StandardPattern();
                    for (int i = 0; i < movingWalls.Length; i++)
                    {
                        movingWalls[i].particles.transform.localPosition = new Vector3(4f, movingWalls[i].particles.transform.localPosition.y, wallVelocity * 1.5f);
                    }
                }
                else
                {
                    StandardPattern();
                }
                break;
            case 2:
                if (waveType == 1)
                {
                    wallVelocity = 14f;
                    startingDistance = 30f;
                    waveDuration = (startingDistance + movingWalls.Length * distanceBetweenWalls) / wallVelocity + 1;
                    StandardPattern();
                    for (int i = 0; i < movingWalls.Length; i++)
                    {
                        movingWalls[i].particles.transform.localPosition = new Vector3(4f, movingWalls[i].particles.transform.localPosition.y, wallVelocity * 1.5f);
                    }
                }
                else if (waveType == 2)
                {
                    wallVelocity = 7f;
                    startingDistance = 30f;
                    distanceBetweenWalls = 16f;
                    waveDuration = (startingDistance + movingWalls.Length / 2 * distanceBetweenWalls) / wallVelocity + 1; 
                    StandardPattern();
                }
                else if (waveType ==3)
                {
                    wallVelocity = 7f;
                    startingDistance = 30f;
                    distanceBetweenWalls = 16f;
                    waveDuration = (startingDistance + movingWalls.Length / 2 * distanceBetweenWalls) / wallVelocity + 1;
                    StandardPattern();
                }
                break;
            default:
                break;
        }
    }

    public void StandardPattern() // the pattern that is usually initiated
    {
        for (int i = 0; i < movingWalls.Length; i++)
        {
            movingWalls[i].particles.transform.localPosition = new Vector3(4f, movingWalls[i].particles.transform.localPosition.y, wallVelocity * 1.5f);
        }

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

        if (waveNumber == 2 && waveType == 2)
        {
            for (int i = 0; i < movingWalls.Length; i++)
            {
                int tempIndex = i / 2;
                switch (i % 4)
                {
                    case 0:
                        movingWalls[i].Setup(new Vector3(startingDistance + distanceBetweenWalls * tempIndex, Random.Range(-3.5f, 3.5f), 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                    case 1:
                        movingWalls[i].Setup(new Vector3(Random.Range(-3.5f, 3.5f), -startingDistance - distanceBetweenWalls * tempIndex, 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                    case 2:
                        movingWalls[i].Setup(new Vector3(-startingDistance - distanceBetweenWalls * tempIndex, Random.Range(-3.5f, 3.5f), 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                    case 3:
                        movingWalls[i].Setup(new Vector3(Random.Range(-3.5f, 3.5f), startingDistance + distanceBetweenWalls * tempIndex, 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                }
            }
        }
        else if (waveNumber == 2 && waveType == 3)
        {
            startingEulers = new Vector3[] {
                new Vector3(0, -90, 0),
                new Vector3(180, -90, 0),
                new Vector3(-90, -90, 0),
                new Vector3(90, -90, 0)
                };
            for (int i = 0; i < movingWalls.Length; i++)
            {
                int tempIndex = i / 2;
                switch (i % 4)
                {
                    case 0:
                        movingWalls[i].Setup(new Vector3(startingDistance + distanceBetweenWalls * tempIndex, Random.Range(0, 3.5f), 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                    case 1:
                        movingWalls[i].Setup(new Vector3(-startingDistance - distanceBetweenWalls * tempIndex, Random.Range(-3.5f, 0), 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                    case 2:
                        movingWalls[i].Setup(new Vector3(Random.Range(-3.5f, 0), -startingDistance - distanceBetweenWalls * tempIndex, 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                    case 3:
                        movingWalls[i].Setup(new Vector3(Random.Range(0, 3.5f), startingDistance + distanceBetweenWalls * tempIndex, 0), startingEulers[i % startingEulers.Length], wallVelocity);
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < movingWalls.Length; i++)
            {
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
        if (waveNumber == 1)
        {
            if (waveType == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    movingWalls[i].guidingHand.SetActive(false);
                }
            }
            waveType++;
            if (waveType >= 3)
            {
                isCurrentPattern = false;
                patternManager.EndWave1();
            }
            else
            {
                WaveStarts();
            }
        }
        else
        {
            waveType++;
            if (waveType >= 4)
            {
                isCurrentPattern = false;
                base.WaveIsDone();
            }
            else
            {
                WaveStarts();
            }
        }
    }
}