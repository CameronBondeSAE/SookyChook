using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    Rigidbody rb;
    public float springStrength=6f;
    public float suspensionLength=1f;
    public float height;

    public float xVelocity;
    public float friction = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Frictional Force for lateral movement
        var localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        xVelocity = localVelocity.x;
        
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
            float force = suspensionLength - height;
            rb.AddForceAtPosition(transform.up * springStrength, transform.position);
        }
        
        Debug.DrawLine(transform.localPosition, hitInfo.point , Color.green);
        
    }

    
}