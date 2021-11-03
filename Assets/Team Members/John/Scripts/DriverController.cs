using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    [Header("Vehicle Reference")]
    public VehicleModel myVehicle;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sending inputs to the vehicle
        myVehicle.steering = Input.GetAxis("Horizontal");
        myVehicle.acceleration = Input.GetAxis("Vertical");
    }
}
