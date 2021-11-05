using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemExample : MonoBehaviour
{
    public float   speed = 100f;
    public Vector2 movement;
    public float   jumpForce = 10f;

    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        MainActions mainActions = new MainActions();
        mainActions.InGame.Enable();
        
        mainActions.InGame.Interact.performed += InteractOnperformed;
        mainActions.InGame.Jump.performed     += JumpOnperformed;
        mainActions.InGame.Movement.performed += MovementOnperformed;
    }

    void MovementOnperformed(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        
        //if (context.performed)
        {
            movement = context.ReadValue<Vector2>();
        }

    }

    void JumpOnperformed(InputAction.CallbackContext context)
    {
        rb.AddForce(0,jumpForce,0, ForceMode.Impulse);
    }

    void InteractOnperformed(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Horizontal and Vertical
        rb.AddRelativeForce(movement.x * speed, 0, movement.y * speed);
        
        
        Debug.Log(rb.velocity);
    }
}
