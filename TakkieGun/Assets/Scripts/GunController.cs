using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float thrustingForce;
    public Vector3 gravtiyVector;

    public float maxFuel;
    public float currentFuel;

    public Jet[] thrusters;

    public Text fuelText;

    private Rigidbody rb;
    private Vector3[] thrusterAxisPositions;
    private Vector3[] thrusterAxisEulers;
    private Vector3[] thrusterDiagonalPositions;
    private Vector3[] thrusterDiagonalEulers;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ApplySettings();

        gravtiyVector = new Vector3(0, 0, 0);

        SetAxisThrusters();
        //SetDiagonalThrusters();
    }

    [ContextMenu("Apply new values")]
    public void ApplySettings()
    {
        SetThrusterPositionVariables();
        SetAxisThrusters();
        UpdateFuelText();
        Physics.gravity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Move Down") && thrusters[0] != null) //upper
        {
            Shoot(thrusters[0]);
            if (!thrusters[0].workingParticles.isPlaying)
                thrusters[0].workingParticles.Play();
        }
        else
        {
            if (!thrusters[0].workingParticles.isStopped)
                thrusters[0].workingParticles.Stop();
        }

        if (Input.GetButton("Move Up") && thrusters[1] != null) //lower
        {
            Shoot(thrusters[1]);
            if (!thrusters[1].workingParticles.isPlaying)
                thrusters[1].workingParticles.Play();
        }
        else
        {
            if (!thrusters[1].workingParticles.isStopped)
                thrusters[1].workingParticles.Stop();
        }

        if (Input.GetButton("Move Left") && thrusters[2] != null) //right
        {
            Shoot(thrusters[2]);
            if (!thrusters[2].workingParticles.isPlaying)
                thrusters[2].workingParticles.Play();
        }
        else
        {
            if (!thrusters[2].workingParticles.isStopped)
                thrusters[2].workingParticles.Stop();
        }

        if (Input.GetButton("Move Right") && thrusters[3] != null) //left
        {
            Shoot(thrusters[3]);
            if (!thrusters[3].workingParticles.isPlaying)
                thrusters[3].workingParticles.Play();
        }
        else
        {
            if (!thrusters[3].workingParticles.isStopped)
                thrusters[3].workingParticles.Stop();
        }
        ApplySettings();
    }

    void Shoot(Jet thrusterObject)
    {
        Vector3 shootingDirection = new Vector3(-thrusterObject.transform.localPosition.x, -thrusterObject.transform.localPosition.y, 0); // the direction is always opposite to the direction the barrel is facing
        Debug.Log("thrusterobject localposition: " + thrusterObject.transform.localPosition + ", shootingDirection: " + shootingDirection);

        rb.AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * thrusterObject.thrustForce), // TransformDirection converts localspace vectors to worldspace values
            thrusterObject.transform.position
            );

        currentFuel -= thrusterObject.thrustForce;
        UpdateFuelText();
    }

    void UpdateFuelText()
    {
        fuelText.text = "Fuel: " + currentFuel + "/" + maxFuel;
    }

    public void SetThrusterPositionVariables()
    {
        thrusterAxisPositions = new Vector3[4] 
        { 
            new Vector3(0, 0.5f, 0), //upper
            new Vector3(0, -0.5f, 0), //lower
            new Vector3(0.5f, 0, 0), //right
            new Vector3(-0.5f, 0, 0) //left
        };

        thrusterAxisEulers = new Vector3[4]
        {
            new Vector3(0, 0, 180), //upper
            new Vector3(0, 0, 0), //lower
            new Vector3(0, 0, 90), //right
            new Vector3(0, 0, -90) //left
        };

        thrusterDiagonalPositions = new Vector3[4] 
        { 
            new Vector3(0.5f, 0.5f, 0), //upper-right
            new Vector3(0.5f, -0.5f, 0), //lower-right
            new Vector3(-0.5f, -0.5f, 0), //lower-left
            new Vector3(-0.5f, 0.5f, 0) //upper-left
        };

        thrusterDiagonalEulers = new Vector3[4]
        {
            new Vector3(0, 0, -135), //upper-right
            new Vector3(0, 0, -45), //lower-right
            new Vector3(0, 0, 45), //lower-left
            new Vector3(0, 0, 135) //upper-left
        };
    }

    public void SetAxisThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            Jet tempThruster = thrusters[i];
            if (tempThruster != null)
            {
                PlaceAxisThrusters(tempThruster, i);
                tempThruster.thrustForce = thrustingForce;
            }
        }
    }

    public void SetDiagonalThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            Jet tempThruster = thrusters[i];
            if (tempThruster != null)
            {
                PlaceDiagonalThrusters(thrusters[i], i);
                tempThruster.thrustForce = thrustingForce;
            }
        }
    }

    public void PlaceAxisThrusters(Jet thruster, int positionIndex)
    {
        Debug.Log("index: " + positionIndex + ", arrayLength: " + thrusterAxisPositions.Length); // length is 0, because it needs to be initialized
        thruster.transform.localPosition = thrusterAxisPositions[positionIndex];
        thruster.transform.eulerAngles = thrusterAxisEulers[positionIndex];
    }

    public void PlaceDiagonalThrusters(Jet thruster, int positionIndex)
    {
        thruster.transform.localPosition = thrusterDiagonalPositions[positionIndex];
        thruster.transform.eulerAngles = thrusterDiagonalEulers[positionIndex];
    }
}
