using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInput : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationalSpeed = 10.0f;
    Rigidbody rb;
    Vector3 localVelocity;
    
    private bool Forward;
    private bool Backward;
    private bool Right;
    private bool Left;
    private bool LookLeft;
    private bool LookRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Forward = InputSystem.GetDevice<Keyboard>().wKey.isPressed;
        Backward = InputSystem.GetDevice<Keyboard>().sKey.isPressed;

        //Rotation
        LookLeft=InputSystem.GetDevice<Keyboard>().aKey.isPressed;
        LookRight=InputSystem.GetDevice<Keyboard>().dKey.isPressed;
        
        //localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        
        
        MoveForward();
        MoveDownward();
        LookRotation();
    }

    void MoveForward()
    {
        if (Forward == true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 1) * speed);
            //frictional force
            rb.AddRelativeForce(new Vector3(0, 0, -1) * speed/2);
        }
    }

    void MoveDownward()
    {
        if (Backward == true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, -1) * speed);
            rb.AddRelativeForce(new Vector3(0, 0, 1) * speed/2);
        }
    }

    void LookRotation()
    {
        //use transform.rotate
        
        if (LookLeft == true)
        {
            rb.AddRelativeTorque(0,-rotationalSpeed,0);
        }
        if (LookRight == true)
        {
            rb.AddRelativeTorque(0,rotationalSpeed,0);
        }
    }
}

