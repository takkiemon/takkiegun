using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusPattern : WallPattern
{
    private Rigidbody rb;
    public float waveCycleLength;

    // Start is called before the first frame update
    void Start()
    {
        
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

        movingWalls[0].transform.position = new Vector3(movingWalls[0].transform.position.x, Mathf.Cos((movingWalls[0].transform.position.x % waveCycleLength) / waveCycleLength * 2f * Mathf.PI) * 3.5f, movingWalls[0].transform.position.z);
        /*
        Debug.Log("x: " + movingWalls[0].transform.position.x + ", Cos: " + Mathf.Cos((movingWalls[0].transform.position.x % 8) / 8 * 2f * Mathf.PI) + ", y: " + Mathf.Cos((movingWalls[0].transform.position.x % 8) / 8 * 2f * Mathf.PI) * 3.5f + ".");
        rb.velocity = new Vector3 (rb.velocity.x, Mathf.Cos((movingWalls[0].transform.position.x % 8) / 8 * 2f * Mathf.PI) * 3.5f, rb.velocity.z);
        */
    }
}
