using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeliveryTruckModel : MonoBehaviour, IVehicle
{
    [Header ("DeliveryTruck Attributes")]
    public Rigidbody rb;

    public float speed = 2000.0f;
    public Transform exitPoint;

    [Header("Wheel Properties")]
    public GameObject Tires;

    public Transform[] wheelVisuals;
    public List<Transform> drivingWheels;
    public List<Transform> steeringWheels;

    [Header("Move Speed")]
    public float acceleration = 0.0f;
    public float maxSteeringAngle = 45.0f;
    private float steering;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //mechanical aspect for wheels
        foreach (Transform drivingWheel in drivingWheels)
        {
            //rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            rb.AddForceAtPosition(drivingWheel.transform.forward * acceleration * speed, drivingWheel.position);
        }

        foreach (Transform steeringWheel in steeringWheels)
        {
            //rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            rb.AddForceAtPosition(steeringWheel.forward*acceleration*speed,steeringWheel.position);
            steeringWheel.localRotation = Quaternion.Euler(0,steering*maxSteeringAngle,0);
        }
        
        //visual aspect for steering wheels
        foreach (Transform v in wheelVisuals)
        {
            v.DOLocalRotateQuaternion(Quaternion.Euler(0, steering * maxSteeringAngle, 0), acceleration);
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
