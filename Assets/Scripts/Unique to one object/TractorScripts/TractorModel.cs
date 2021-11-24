using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorModel : MonoBehaviour, IVehicle
{
    [Header("Vehicle Attributes")]
    public Rigidbody rb;
    public float speed = 5f;
    public float turnRadius = 10f;
    //public float frictionAmount = 5f;
    public Transform exitPoint;
    public GameObject wheels;

    [Header("Ragdoll Physics - when jumping out of tractor")]
    [SerializeField]
    float reductionSpeed = 0.05f;
    [SerializeField]
    float reductionTimer = 0.5f;

    bool playerInTractor = false;
    float acceleration;
    float steering;

    //[Space]
    //public Vector3 localVelocity;
    //public float xVelocity;
    
    [Space]
    [Header("Turning Wheels Only")]
    public List<Transform> steeringWheels = new List<Transform>();

    [Header("Driving Wheels Only")]
    public List<Transform> drivingWheels = new List<Transform>();

    [Header("For Reference Only")]
    public float steeringAngle;

    //Events
    public event Action EnterTractorEvent;
    public event Action ExitTractorEvent;

    void Start()
    {
        wheels.SetActive(false);
    }

    private void Update()
    {
        //This vehicles velocity
        //localVelocity = transform.InverseTransformDirection(rb.velocity);
        //xVelocity = localVelocity.x;

        //Add a force to the vehicles local x velocity (left & right) so vehicle can only travel forwards
        //rb.AddRelativeForce(Vector3.right * xVelocity * -frictionAmount);

        //Only reduce speed when the player is not in the tractor & the tractor is moving
        if(!playerInTractor)
        {
            if(acceleration > 0)
            {
                StartCoroutine(ReduceSpeed());
            }
        }

        //Once the player has stopped moving and player is not in tractor - lock it back up
        if (rb.velocity.magnitude < 0.1f && !playerInTractor)
        {
            wheels.SetActive(false);
            rb.isKinematic = true;
        }
    }

    //Using a coroutine to reduce speed so the function can pause after each speed reduction - allows the tractor to keep moving before reaching zero
    IEnumerator ReduceSpeed()
    {
        if(acceleration > 0)
        {
            acceleration -= reductionSpeed;
            yield return new WaitForSeconds(reductionTimer);
        }
    }


    void FixedUpdate()
    {
        steeringAngle = steering * turnRadius;

        foreach (Transform steeringWheel in steeringWheels)
        {
            steeringWheel.localRotation = Quaternion.Euler(0, steeringAngle, 0);
            rb.AddForceAtPosition(steeringWheel.forward * acceleration * speed, steeringWheel.position, ForceMode.Force);
        }

        foreach (Transform driveWheel in drivingWheels)
        {
            //rb.AddRelativeForce(Input.GetAxis("Vertical") * driveWheel.forward * speed);
            rb.AddForceAtPosition(driveWheel.forward * acceleration * speed, driveWheel.position, ForceMode.Force);
        }
    }

    public void Enter()
    {
        //Invoke event for all view related functionality
        EnterTractorEvent?.Invoke();

        //Model Functionality
        wheels.SetActive(true);
        rb.isKinematic = false;
        playerInTractor = true;
    }

    public void Exit()
    {
        ExitTractorEvent?.Invoke();

        playerInTractor = false;
        //wheels.SetActive(false);
        //rb.isKinematic = true;
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
