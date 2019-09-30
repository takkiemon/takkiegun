using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // the offset of the barrel relative to the rest of the gun (mostly relative to the center (of gravity))
    public Vector3 shipThrusterRedOffset; // let's assume that the gun is pointing to the left (as if you're holding a gun with your right hand and inspecting it)
    public Vector3 shipThrusterGreenOffset; // the other barrel
    public Vector3 shipThrusterBlueOffset; // let's assume that the gun is pointing to the left (as if you're holding a gun with your right hand and inspecting it)
    public Vector3 shipThrusterYellowOffset; // the other barrel

    public float[] offSetHeights; // this array holds the y-values that changes the offsets of the two barrels (for now I only want to change one dimension)
    public Vector3 gravityVector;

    public int redPosition;
    public int greenPosition;

    public float gunGizmoRadius;
    public float thrustingForce;

    public ParticleSystem jetRed;
    public ParticleSystem jetGreen;
    public ParticleSystem jetBlue;
    public ParticleSystem jetYellow;

    public float maxFuel;
    public float currentFuel;

    public GameObject[] thrusters;
    public GameObject thrusterRed;
    public GameObject thrusterGreen;
    public GameObject thrusterBlue;
    public GameObject thrusterYellow;

    public Text fuelText;

    private Rigidbody rb;
    private Vector3[] thrusterAxisPositions;
    private Vector3[] thrusterDiagonalPositions;
    private Vector3[] thrusterAxisEulers;
    private Vector3[] thrusterDiagonalEulers;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ApplySettings();
        //redPosition = 0;
        //greenPosition = 0;

        SetThrusterPositionVariables();
        SetAxisThrusters();
        SetDiagonalThrusters();

        /*
        offSetHeights = new float[3];
        offSetHeights[0] = 0f;
        offSetHeights[1] = -0.25f;
        offSetHeights[2] = 0.25f;
        */
    }

    [ContextMenu("Apply new values")]
    public void ApplySettings()
    {
        thrusterRed.transform.localPosition = shipThrusterRedOffset;
        thrusterGreen.transform.localPosition = shipThrusterGreenOffset;
        Physics.gravity = gravityVector;
        UpdateFuelText();
    }

    public void ApplySettings(Vector3 redThrusterOffset, Vector3 greenThrusterOffset, Vector3 gravity)
    {
        shipThrusterRedOffset = redThrusterOffset;
        shipThrusterGreenOffset = greenThrusterOffset;
        gravityVector = gravity;
        ApplySettings();
    }

    public void ChangeRedPosition(float newOffset)
    {
        shipThrusterRedOffset.y = newOffset;
    }

    public void ChangeGreenPosition(float newOffset)
    {
        shipThrusterGreenOffset.y = newOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("FireRed"))
        {
            Shoot(thrusterRed.transform.localPosition, thrusterRed, thrustingForce);
            if (!jetRed.isPlaying)
                jetRed.Play();
        }
        else
        {
            if (!jetRed.isStopped)
                jetRed.Stop();
        }

        if (Input.GetButton("FireGreen"))
        {
            Shoot(thrusterGreen.transform.localPosition, thrusterGreen, thrustingForce);
            if (!jetGreen.isPlaying)
            {
                jetGreen.Play();
            }
        }
        else
        {
            if (!jetGreen.isStopped)
            {
                jetGreen.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (redPosition < offSetHeights.Length - 1)
            {
                redPosition++;
            }
            else
            {
                redPosition = 0;
            }

            ChangeRedPosition(offSetHeights[redPosition]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (greenPosition < offSetHeights.Length - 1)
            {
                greenPosition++;
            }
            else
            {
                greenPosition = 0;
            }

            ChangeGreenPosition(offSetHeights[greenPosition]);
        }
        ApplySettings();
    }

    void Shoot(Vector3 gunThrusterOffset, GameObject thrusterObject, float thustingForce)
    {
        Vector3 shootingDirection = new Vector3(-gunThrusterOffset.x, 0, 0); // the direction is always opposite to the direction the barrel is facing

        rb.AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * thustingForce), // TransformDirection converts localspace vectors to worldspace values
            thrusterObject.transform.position
            );

        currentFuel -= thustingForce;
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
            new Vector3(0, 0, -90), //right
            new Vector3(0, 0, 90) //left
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
            if (thrusters[i] != null)
                PlaceAxisThrusters(thrusters[i], i);
        }
    }

    public void SetDiagonalThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            if (thrusters[i] != null)
                PlaceDiagonalThrusters(thrusters[i], i);
        }
    }

    public void PlaceAxisThrusters(GameObject thruster, int positionIndex)
    {
        thruster.transform.localPosition = thrusterAxisPositions[positionIndex];
        thruster.transform.eulerAngles = thrusterAxisEulers[positionIndex];
    }

    public void PlaceDiagonalThrusters(GameObject thruster, int positionIndex)
    {
        thruster.transform.localPosition = thrusterDiagonalPositions[positionIndex];
        thruster.transform.eulerAngles = thrusterDiagonalEulers[positionIndex];
    }
}
