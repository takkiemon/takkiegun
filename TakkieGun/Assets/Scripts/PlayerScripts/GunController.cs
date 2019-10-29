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

    public List<Rigidbody> collidingBodies;

    // Start is called before the first frame update
    void Start()
    {
        ApplySettings();
        thrustersSetup();

        collidingBodies = new List<Rigidbody>();
        gravityVector = new Vector3(0, 0, 0);
    }

    public void thrustersSetup()
    {
        thrusters[0].inputName = "Move Down"; // red
        thrusters[1].inputName = "Move Up"; // green
        thrusters[2].inputName = "Move Left"; // blue
        thrusters[3].inputName = "Move Right"; // yellow


        SetAxisThrusters();
    }

    [ContextMenu("Apply new values")]
    public void ApplySettings()
    {
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

    public void SetAxisThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            Jet tempThruster = thrusters[i];
            if (tempThruster != null)
            {
                //PlaceAxisThrusters(tempThruster, i);
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
                //PlaceDiagonalThrusters(thrusters[i], i);
                tempThruster.thrustForce = thrustingForce;
            }
        }
    }

    public void OnDeathTrigger(Collider killerObject)
    {
        Debug.Log("You died, son. You got hit by " + killerObject.transform.name + ".");
        // maybe add a foreach() where I can make it debug something along the lines of "you were hit by object A and object B and object C." etc. where all the colliders that are stored in the list will be spelled out.
    }

    private void OnCollisionEnter(Collision collision)
    {
        collidingBodies.Add(collision.rigidbody);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collidingBodies.Contains(collision.rigidbody))
        {
            collidingBodies.Remove(collision.rigidbody);
        }
    }
}
