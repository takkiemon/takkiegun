using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // the offset of the barrel relative to the rest of the gun (mostly relative to the center (of gravity))
    public Vector3 gunBarrelOffset; // let's assume that the gun is pointing to the left (as if you're holding a gun with your right hand and inspecting it)
    public float gunGizmoRadius;
    public float shootingForce;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 shootingDirection = new Vector3(-gunBarrelOffset.x, 0, 0); // the direction is always opposite to the direction the barrel is facing
        //GetComponent<Rigidbody>().AddForceAtPosition(shootingDirection.normalized * shootingForce, transform.localPosition + gunBarrelOffset);
        rb.AddForceAtPosition((-transform.localPosition).normalized * shootingForce, transform.localPosition + gunBarrelOffset);
        GetComponent<Rigidbody>().AddForceAtPosition(
            transform.TransformDirection(shootingDirection.normalized * shootingForce), // TransformDirection converts localspace vectors to worldspace values
            transform.localPosition + gunBarrelOffset
            );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.localPosition + gunBarrelOffset, gunGizmoRadius);
    }
}
