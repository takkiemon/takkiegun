using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPatternManager : MonoBehaviour
{
    public OneWayPattern oneWayScript;
    public DoublePattern doubleScript;
    public GroupPattern groupScript;

    private int currentPattern;

    public GameObject[] movingWalls = new GameObject[5];
    private Vector3 defaultWallPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPattern(int patternNumber)
    {
        currentPattern = patternNumber;
        switch(patternNumber)
        {
            case 0:
                oneWayScript.enabled = false;
                doubleScript.enabled = false;
                groupScript.enabled = false;
                break;
            case 1:
                oneWayScript.enabled = true;
                doubleScript.enabled = false;
                groupScript.enabled = false;
                break;
            case 2:
                oneWayScript.enabled = false;
                doubleScript.enabled = true;
                groupScript.enabled = false;
                break;
            case 3:
                oneWayScript.enabled = false;
                doubleScript.enabled = false;
                groupScript.enabled = true;
                break;
            default:
                break;
        }
    }
}
