using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPattern : WallPattern
{
    public WallPatternManager patternManager;
    public GameObject[] movingWalls;
    private Vector3 defaultWallPosition;

    // Start is called before the first frame update
    void Start()
    {
        defaultWallPosition = new Vector3(16, 0, 0);
        
        foreach (GameObject wall in movingWalls)
        {
            wall.transform.position = defaultWallPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
