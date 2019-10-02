using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float thrustingForce;
    public Vector3 gravityVector;

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
        ApplySettings();

        thrustersSetup();

        gravityVector = new Vector3(0, 0, 0);
    }

    public void thrustersSetup()
    {
        thrusters[0].inputName = "Move Down"; // red
        thrusters[1].inputName = "Move Up"; // green
        thrusters[2].inputName = "Move Left"; // blue
        thrusters[3].inputName = "Move Right"; // yellow


        SetAxisThrusters();
        //SetDiagonalThrusters();
    }

    [ContextMenu("Apply new values")]
    public void ApplySettings()
    {
        SetThrusterPositionVariables();
        SetAxisThrusters();
        Physics.gravity = gravityVector;
    }

    // Update is called once per frame
    void Update()
    {
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
