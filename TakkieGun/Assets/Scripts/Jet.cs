using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public string inputName;
    public Vector3 thrusterOffset;

    public bool isBroken;
    //public int position;

    public float thrustForce;

    public ParticleSystem workingParticles;
    public ParticleSystem smokeParticles;
    private ParticleSystem currentParticles;
    private GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
        transform.localPosition = thrusterOffset;
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(inputName)) //upper
        {
            Shoot();
            if (!currentParticles.isPlaying)
                currentParticles.Play();
        }
        else
        {
            if (!currentParticles.isStopped)
                currentParticles.Stop();
        }
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
        
        parentObject.GetComponent<Rigidbody>().AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * thrustForce), // TransformDirection converts localspace vectors to worldspace values
            gameObject.transform.position
            );

        parentObject.GetComponent<GunController>().currentFuel -= thrustForce;
    }

    public void Activate()
    {
        currentParticles = workingParticles;
        isBroken = false;
        smokeParticles.Stop();
        workingParticles.Play();
    }

    public void ShutDown()
    {
        currentParticles = smokeParticles;
        isBroken = true;
        smokeParticles.Play();
        workingParticles.Stop();
    }
}
