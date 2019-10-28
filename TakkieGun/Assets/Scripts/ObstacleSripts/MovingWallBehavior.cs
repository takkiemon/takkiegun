using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MovingWallBehavior : MonoBehaviour
{
    public GameObject[] wallPartitions = new GameObject[2];
    public GameObject guidingHand;
    Rigidbody tempRB;

    // Start is called before the first frame update
    void Start()
    {
        tempRB = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Vector3 startingPosition, Vector3 startingAngle, float speed)
    {
        gameObject.transform.position = startingPosition;
        gameObject.transform.eulerAngles = startingAngle;
        tempRB = transform.GetComponent<Rigidbody>();
        tempRB.velocity = speed * tempRB.transform.forward;
    }
}
