using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // the offset of the barrel relative to the rest of the gun (mostly relative to the center (of gravity))
    public Vector3 gunbarrelRedOffset; // let's assume that the gun is pointing to the left (as if you're holding a gun with your right hand and inspecting it)
    public Vector3 gunbarrelGreenOffset; // the other barrel
    public Vector3 gunbarrelBlueOffset; // let's assume that the gun is pointing to the left (as if you're holding a gun with your right hand and inspecting it)
    public Vector3 gunbarrelYellowOffset; // the other barrel

    public float[] offSetHeights; // this array holds the y-values that changes the offsets of the two barrels (for now I only want to change one dimension)
    public Vector3 gravityVector;

    public int redPosition;
    public int greenPosition;

    public float gunGizmoRadius;
    public float shootingForce;

    public ParticleSystem jetRed;
    public ParticleSystem jetGreen;
    public ParticleSystem jetBlue;
    public ParticleSystem jetYellow;

    public float maxFuel;
    public float currentFuel;

    public GameObject barrelRed;
    public GameObject barrelGreen;
    public GameObject barrelBlue;
    public GameObject barrelYellow;

    public Text fuelText;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ApplySettings();
        redPosition = 0;
        greenPosition = 0;

        offSetHeights = new float[3];
        offSetHeights[0] = 0f;
        offSetHeights[1] = -0.25f;
        offSetHeights[2] = 0.25f;
    }

    [ContextMenu("Apply new values")]
    public void ApplySettings()
    {
        barrelRed.transform.localPosition = gunbarrelRedOffset;
        barrelGreen.transform.localPosition = gunbarrelGreenOffset;
        Physics.gravity = gravityVector;
        UpdateFuelText();
    }

    public void ApplySettings(Vector3 redBarrelOffset, Vector3 greenBarrelOffset, Vector3 gravity)
    {
        gunbarrelRedOffset = redBarrelOffset;
        gunbarrelGreenOffset = greenBarrelOffset;
        gravityVector = gravity;
        ApplySettings();
    }

    public void ChangeRedPosition(float newOffset)
    {
        gunbarrelRedOffset.y = newOffset;
    }

    public void ChangeGreenPosition(float newOffset)
    {
        gunbarrelGreenOffset.y = newOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("FireRed"))
        {
            Shoot(barrelRed.transform.localPosition, barrelRed, shootingForce);
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
            Shoot(barrelGreen.transform.localPosition, barrelGreen, shootingForce);
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

    void Shoot(Vector3 gunBarrelOffset, GameObject barrelObject, float shootingForce)
    {
        Vector3 shootingDirection = new Vector3(-gunBarrelOffset.x, 0, 0); // the direction is always opposite to the direction the barrel is facing

        rb.AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * shootingForce), // TransformDirection converts localspace vectors to worldspace values
            barrelObject.transform.position
            );

        currentFuel -= shootingForce;
        UpdateFuelText();
    }

    void UpdateFuelText()
    {
        fuelText.text = "Fuel: " + currentFuel + "/" + maxFuel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(barrelGreen.transform.position, gunGizmoRadius);
    }
}
