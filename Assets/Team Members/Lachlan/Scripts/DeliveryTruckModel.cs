using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTruckModel : MonoBehaviour, IVehicle
{
    [Header ("DeliveryTruck Attributes")]
    public Rigidbody rb;

    public float speed = 1500.0f;
    public Transform exitPoint;

    public GameObject Tires;
    public List<Wheel> drivingWheels;
    public List<Wheel> steeringWheels;

    public float acceleration;
    public float maxSteeringAngle = 70.0f;
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
            //rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            rb.AddForceAtPosition(drivingWheel.transform.forward * acceleration * speed, drivingWheel.transform.position);
        }

        foreach (Wheel steeringWheel in steeringWheels)
        {
            //rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            rb.AddForceAtPosition(steeringWheel.transform.forward*acceleration*speed,steeringWheel.transform.position);
            steeringWheel.transform.rotation = Quaternion.Euler(0,steering*maxSteeringAngle,0);
        }
    }
    
    public void Enter()
    {
        Tires.SetActive(true);
        rb.isKinematic = false;
    }

    public void Exit()
    {
        Tires.SetActive(false);
    }

    public void Steer(float amount)
    {
        steering = amount;
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
