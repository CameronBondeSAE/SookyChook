using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wheel : MonoBehaviour
{
    public Rigidbody rb;
    public float springStrength=6f;
    public float suspensionLength=1f;
    public float height;
    public float force = 0.0f;

    public float xVelocity;
    public float friction = 5f;

    public Vector3 localVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        xVelocity = localVelocity.x;
        
        //Frictional Force for lateral movement
        rb.AddRelativeForce(Vector3.right*xVelocity*-friction);
        
        //RayCasting
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;
        RaycastHit hitInfo = new RaycastHit();
        Physics.Raycast(ray, out hitInfo, suspensionLength);

        height = hitInfo.distance;
        //height = Vector3.Distance (ray.origin, hitInfo.point);

        if (hitInfo.collider==true)
        {
            height = hitInfo.distance;
            //force = maxHeight - height;
            //force *= maxForce;
            //float force = suspensionLength - height;
            rb.AddForceAtPosition(transform.up * springStrength, transform.position);
        }
        
        Debug.DrawLine(transform.localPosition, hitInfo.point , Color.green);
        
    }

    
}