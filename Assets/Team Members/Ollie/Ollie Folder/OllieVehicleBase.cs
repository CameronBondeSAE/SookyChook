using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllieVehicleBase : MonoBehaviour, IDrivable
{
    public bool grounded;
    public Rigidbody rb;
    public float forwardSpeed;
    public float turnSpeed;
    public Vector3 localVelocity;
    public bool playerInVehicle;
    public Transform exitPoint;
    public GameObject car;

    public delegate void ExitVehicle();

    public static event ExitVehicle exitVehicleEvent;

    private void Update()
    {
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        if (rb.velocity == (Vector3.zero))
        {
            exitVehicleEvent?.Invoke();
        }
    }

    public void Forward()
    {
        if (grounded && playerInVehicle)
        {
            rb.AddRelativeForce(0, 0, forwardSpeed);
        }
    }

    public void Backward()
    {
        if (grounded && playerInVehicle)
        {
            rb.AddRelativeForce(0,0,-forwardSpeed);
        }
    }

    public void Left()
    {
        if (grounded && playerInVehicle)
        {
            rb.AddRelativeForce(-localVelocity/5);
            rb.AddRelativeTorque(0, -turnSpeed,0);
        }
    }
    
    public void Right()
    {
        if (grounded && playerInVehicle)
        {
            rb.AddRelativeForce(-localVelocity/5);
            rb.AddRelativeTorque(0, turnSpeed,0);
        }
    }

    public void Enter()
    {
        playerInVehicle = true;
        car.SetActive(true);
    }

    public void Exit()
    {
        playerInVehicle = false;
        //car.SetActive(false);
    }

    public void Steer(float amount)
    {
        if(amount >= 0)
            Left();
        else
            Right();
    }

    public void Accelerate(float amount)
    {
        if(amount >= 0)
            Forward();
        else
            Backward();
    }
    

    public Transform GetVehicleExitPoint()
    {
        return exitPoint;
    }

    public bool canEnter()
    {
        if(playerInVehicle)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
