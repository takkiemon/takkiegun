using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public string inputName;

    public bool isBroken;
    private bool isAlreadyBroken;

    public float thrustForce;

    public ParticleSystem workingParticles;
    public ParticleSystem smokeParticles;
    private ParticleSystem currentParticles;
    private GameObject parentObject;

    public AudioSource thrustContinuousSound;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
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

        if (Input.GetButtonDown(inputName))
        {
            thrustContinuousSound.Play();
        }

        if (Input.GetButtonUp(inputName))
        {
            thrustContinuousSound.Stop();
        }

        if (isBroken && !isAlreadyBroken)
        {
            ShutDown();
        }
    }

    void Shoot()
    {
        Vector3 shootingDirection = new Vector3(-gameObject.transform.localPosition.x, -gameObject.transform.localPosition.y, 0); // the direction is always opposite to the direction the barrel is facing
        parentObject.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * thrustForce);
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