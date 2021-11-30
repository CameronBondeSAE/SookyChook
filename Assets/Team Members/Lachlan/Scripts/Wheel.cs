using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wheel : MonoBehaviour
{
    [Header("Vehicle Attributes")]
    public Rigidbody rb;
    [Tooltip("Amount of force repelling ground")]
    public float springStrength=2000f;
    public float suspensionLength=0.5f;
    public float height =0.5f;

    [Header("ReadOnly Attributes")]
    public float xVelocity;
    public float friction = 50.0f;
    public Vector3 localVelocity;
    public float force = 0.0f;
    
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
        RaycastHit hitInfo;
        hitInfo = new RaycastHit();
        Physics.Raycast(transform.position, -transform.up, out hitInfo, suspensionLength, 255, QueryTriggerInteraction.Ignore);

        height = hitInfo.distance;
        height = Vector3.Distance (ray.origin, hitInfo.point);

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