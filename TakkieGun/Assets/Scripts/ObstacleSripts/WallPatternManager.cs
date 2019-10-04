using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPatternManager : MonoBehaviour
{
    public OneWayPattern oneWayScript;
    public DoublePattern doubleScript;
    public GroupPattern groupScript;

    public GameObject[] movingWalls = new GameObject[5];
    private Vector3 defaultWallPosition;

    // Start is called before the first frame update
    void Start()
    {
        defaultWallPosition = new Vector3(0, 16, 0);

        foreach(GameObject wall in movingWalls)
        {
            wall.transform.position = defaultWallPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
