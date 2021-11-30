using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTruckModel : MonoBehaviour, IVehicle
{
    [Header ("DeliveryTruck Attributes")]
    public Rigidbody rb;

    public float speed;
    public Transform exitPoint;

    public List<Wheel> drivingWheels;
    public List<Wheel> steeringWheels;

    private float acceleration;
    private float steering;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Wheel drivingWheel in drivingWheels)
        {
            rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
        }

        foreach (Wheel steeringWheel in steeringWheels)
        {
            rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            
            steeringWheel.transform.rotation = Quaternion.Euler(0,steering,0);
        }
    }
    
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Steer(float amount)
    {
        //steering = amount;
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
