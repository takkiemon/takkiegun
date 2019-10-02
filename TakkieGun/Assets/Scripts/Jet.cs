using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public Vector3 thrusterOffset;

    bool isBroken;
    //public int position;

    public float thrustForce;

    public ParticleSystem workingParticles;
    public ParticleSystem smokeParticles;
    private ParticleSystem currentParticles;
    private GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = GetComponentInParent<GameObject>();
        transform.localPosition = thrusterOffset;
        isBroken = false;
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

    void Shoot()
    {
        Vector3 shootingDirection = new Vector3(-gameObject.transform.localPosition.x, -gameObject.transform.localPosition.y, 0); // the direction is always opposite to the direction the barrel is facing
        Debug.Log("thrusterobject localposition: " + gameObject.transform.localPosition + ", shootingDirection: " + shootingDirection);

        parentObject.GetComponent<Rigidbody>().AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * thrustForce), // TransformDirection converts localspace vectors to worldspace values
            gameObject.transform.position
            );

        parentObject.GetComponent<GunController>().currentFuel -= thrustForce;
    }
}
