using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public Vector3 thrusterOffset;

    //public int position;

    public float thrustForce;

    public ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = thrusterOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Place(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Place(Vector3 position, Vector3 rotation)
    {
        transform.localPosition = position;
        transform.eulerAngles = rotation;
    }
}
