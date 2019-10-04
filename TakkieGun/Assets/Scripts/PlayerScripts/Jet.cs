using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public string inputName;
    public Vector3 thrusterOffset;

    public bool isBroken;
    private bool isAlreadyBroken;
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
        isAlreadyBroken = false;
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

        if (isBroken && !isAlreadyBroken)
        {
            ShutDown();
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
        parentObject.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * thrustForce);

        parentObject.GetComponent<GunController>().currentFuel -= thrustForce;
    }

    public void Activate()
    {
        smokeParticles.Stop();
        currentParticles = workingParticles;
        isBroken = false;
        isAlreadyBroken = false;
    }

    public void ShutDown()
    {
        isAlreadyBroken = true;
        workingParticles.Stop();
        currentParticles = smokeParticles;
        isBroken = true;
        thrustForce = 5f;
    }
}