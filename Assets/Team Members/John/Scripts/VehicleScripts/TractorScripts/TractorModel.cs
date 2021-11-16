using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorModel : MonoBehaviour, IVehicleBase
{
    [Header("Vehicle Attributes")]
    public Rigidbody rb;
    public float speed = 5f;
    public float turnRadius = 10f;
    public float frictionAmount = 5f;
    public Transform exitPoint;
    public float acceleration;
    public float steering;

    [Space]
    public Vector3 localVelocity;
    public float xVelocity;
    
    [Space]
    [Header("Turning Wheels Only")]
    public List<Transform> steeringWheels = new List<Transform>();

    [Header("Driving Wheels Only")]
    public List<Transform> drivingWheels = new List<Transform>();

    //Events
    public event Action EnterTractorEvent;
    public event Action ExitTractorEvent;


    private void Update()
    {
        //This vehicles velocity
        //localVelocity = transform.InverseTransformDirection(rb.velocity);
        //xVelocity = localVelocity.x;

        //Add a force to the vehicles local x velocity (left & right) so vehicle can only travel forwards
        //rb.AddRelativeForce(Vector3.right * xVelocity * -frictionAmount);
    }
    void FixedUpdate()
    {
        float steeringAngle = steering * turnRadius;

        foreach (Transform steeringWheel in steeringWheels)
        {
            steeringWheel.localRotation = Quaternion.Euler(0, steeringAngle, 0);
            rb.AddForceAtPosition(steeringWheel.forward * acceleration * speed, steeringWheel.position);
        }

        foreach (Transform driveWheel in drivingWheels)
        {
            //rb.AddRelativeForce(Input.GetAxis("Vertical") * driveWheel.forward * speed);
            rb.AddForceAtPosition(driveWheel.forward * acceleration * speed, driveWheel.position);
        }
    }

    public void Enter()
    {
        EnterTractorEvent?.Invoke();
    }

    public void Exit()
    {
        ExitTractorEvent?.Invoke();
    }

    public void Steer(float amount)
    {
        //throw new System.NotImplementedException();
    }

    public void Accelerate(float amount)
    {
        acceleration = amount;
    }

    public Transform GetVehicleExitPoint()
    {
        return exitPoint;
    }
}
