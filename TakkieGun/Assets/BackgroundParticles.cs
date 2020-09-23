using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticles : MonoBehaviour
{
    public float warpspeedDuration;
    private float warpspeedTimer;
    private bool isWarpSpeed;
    public ParticleSystem particles;
    ParticleSystem.MainModule particlesMain;
    ParticleSystem.EmissionModule particleEmission;
    ParticleSystem.ShapeModule particleShape;
    ParticleSystemRenderer particleRenderer;

    // Start is called before the first frame update
    void Start()
    {
        isWarpSpeed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DefaultState()
    {
        particlesMain.startSpeed = 1;
        particlesMain.loop = true;
        particlesMain.prewarm = true;
        particlesMain.startLifetime = 300;
        particlesMain.maxParticles = 500;
        particleEmission.rateOverTime = 1;
        particleRenderer.lengthScale = 1;
    }

    void StartWarpspeed()
    {
        if (!isWarpSpeed)
        {
            warpspeedTimer = warpspeedDuration;
            //TODO: 
        }
        else
        {
            //TODO: play the ticks of warpspeed (ramp up the speed and smearing and keep up the high speed and then come down again)
        }
    }
}
