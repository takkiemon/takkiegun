using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPattern : MonoBehaviour
{
    public WallPatternManager patternManager;
    public GameObject[] movingWalls;
    private Vector3 defaultWallPosition;

    public Vector3 wallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        defaultWallPosition = new Vector3(16, 0, 0);
        for (int i = 0; i < movingWalls.Length; i++)
        {
            movingWalls[i].transform.position = new Vector3(defaultWallPosition.x + i * (movingWalls[i].transform.localScale.x + 1), defaultWallPosition.y + (i * movingWalls[i].transform.localScale.y + 1), defaultWallPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTheWallMovin()
    {
        movingWalls[0].GetComponent<Rigidbody>().velocity = wallSpeed;
    }

    public void SetMovementVariables(Vector3 startingPosition, float movementSpeed)
    {

    }
}
