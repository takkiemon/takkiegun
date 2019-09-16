using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // the offset of the barrel relative to the rest of the gun (mostly relative to the center (of gravity))
    public Vector3 gunbarrelRedOffset; // let's assume that the gun is pointing to the left (as if you're holding a gun with your right hand and inspecting it)
    public Vector3 gunbarrelGreenOffset; // the other barrel

    public float gunGizmoRadius;

    public float shootingForce;

    public GameObject barrelRed;
    public GameObject barrelGreen;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("red barrel offstet: " + gunbarrelRedOffset);
        barrelRed.transform.localPosition = gunbarrelRedOffset;
        barrelGreen.transform.localPosition = gunbarrelGreenOffset;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot(barrelRed.transform.localPosition, barrelRed, shootingForce);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shoot(barrelGreen.transform.localPosition, barrelGreen, shootingForce);
        }
    }

    void Shoot(Vector3 gunBarrelOffset, GameObject barrelObject, float shootingForce)
    {
        Vector3 shootingDirection = new Vector3(-gunBarrelOffset.x, 0, 0); // the direction is always opposite to the direction the barrel is facing
        rb.AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * shootingForce), // TransformDirection converts localspace vectors to worldspace values
            barrelObject.transform.position
            );
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(barrelGreen.transform.position, gunGizmoRadius);
    }
}
