using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallBehavior : MonoBehaviour
{
    public GameObject[] wallPartitions = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Vector3 startingPosition, Vector3 startingAngle, float speed)
    {
        gameObject.transform.position = startingPosition;
        gameObject.transform.eulerAngles = startingAngle;
    }
}
