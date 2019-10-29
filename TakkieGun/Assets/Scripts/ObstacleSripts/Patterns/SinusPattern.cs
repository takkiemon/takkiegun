using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusPattern : WallPattern
{
    public float wavelength;
    public float amplitude; // standard is 3.5, since a wave with that amplitude would cover the whole arena
    public int waveType; // 1 is normal wavy alignment, 2 is crazywave, with vertical movement

    private float tempX;
    private float yValuetemp;
    private float randomCosineOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void WaveStarts()
    {
        Debug.Log("Sine Pattern started.");

        randomCosineOffset = Random.Range(-1f, 1f);

        switch (waveType)
        {
            case 1:
                wavelength = 24f;
                distanceBetweenWalls = .8f;
                wallVelocity = 3f;
                break;
            case 2:
                wavelength = 16f;
                distanceBetweenWalls = 0.8f;
                wallVelocity = 5f;
                for (int i = 0; i < movingWalls.Length; i++)
                {
                    movingWalls[i].particles.transform.localPosition = new Vector3(4f, movingWalls[i].particles.transform.localPosition.y, wavelength);
                }
                break;
        }

        startingEulers = new Vector3[] {
            new Vector3(0, -90, 0),
            new Vector3(180, -90, 0),
        };

        timePassed = 0;
        waveDuration = (startingDistance + movingWalls.Length * distanceBetweenWalls) / wallVelocity + 3f;

        for (int i = 0; i < movingWalls.Length; i++)
        {
            switch (i % 2)
            {
                case 0:
                    yValuetemp = Mathf.Cos(((tempX % wavelength) / wavelength + randomCosineOffset) * 2f * Mathf.PI) * amplitude;
                    tempX = startingDistance + distanceBetweenWalls * i;
                    Debug.Log("cosine offset: " + randomCosineOffset + "y-value: " + yValuetemp);
                    movingWalls[i].Setup(new Vector3(tempX, yValuetemp, 0f), startingEulers[i % startingEulers.Length], wallVelocity);
                    break;
                case 1:
                    yValuetemp = Mathf.Cos(((tempX % wavelength) / wavelength - randomCosineOffset) * 2f * Mathf.PI) * amplitude;
                    tempX = -startingDistance - distanceBetweenWalls * (i - 1);
                    movingWalls[i].Setup(new Vector3(tempX, yValuetemp, 0f), startingEulers[i % startingEulers.Length], wallVelocity);
                    break;
            }
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
                Debug.Log("Sine Pattern ended.");
                WaveIsDone();
            }

            if (waveType == 2)
            {
                for (int i = 0; i < movingWalls.Length; i++)
                {
                    movingWalls[i].transform.position = new Vector3(movingWalls[i].transform.position.x, Mathf.Cos(((movingWalls[i].transform.position.x % wavelength) / wavelength + randomCosineOffset) * 2f * Mathf.PI) * amplitude, movingWalls[i].transform.position.z);
                }
            }
        }
        /*
        Debug.Log("x: " + movingWalls[0].transform.position.x + ", Cos: " + Mathf.Cos((movingWalls[0].transform.position.x % 8) / 8 * 2f * Mathf.PI) + ", y: " + Mathf.Cos((movingWalls[0].transform.position.x % 8) / 8 * 2f * Mathf.PI) * 3.5f + ".");
        rb.velocity = new Vector3 (rb.velocity.x, Mathf.Cos((movingWalls[0].transform.position.x % 8) / 8 * 2f * Mathf.PI) * 3.5f, rb.velocity.z);
        */
    }

    public override void WaveIsDone()
    {
        if (waveType >= 2)
        {
            base.WaveIsDone();
        }
        else
        {
            waveType++;
            WaveStarts();
        }
    }
}
