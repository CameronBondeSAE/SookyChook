using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DriverController : MonoBehaviour
{
    [Header("Vehicle Reference")]
    public VehicleModel myVehicle;

    [Header("For Reference Only")]
    public Vector2 playerInput;

    private void Start()
    {
        DriveControls driveControls = new DriveControls();
        driveControls.InGame.Enable();

        driveControls.InGame.Movement.performed += PlayerMovement;
        driveControls.InGame.Movement.canceled += PlayerMovement;
    }

    void PlayerMovement(InputAction.CallbackContext value)
    {
        playerInput = value.ReadValue<Vector2>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Sending inputs to the vehicle
        myVehicle.steering = playerInput.x;
        myVehicle.acceleration = playerInput.y;
    }
}
