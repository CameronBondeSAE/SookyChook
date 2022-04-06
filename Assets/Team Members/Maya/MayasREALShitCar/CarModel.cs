using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel : MonoBehaviour, IDrivable
{
    public Transform exitPoint;
    public bool inCar;
    public float speed;
    void Start()
    {
        
    }
    
    void Update()
    {


    }

    public void Enter()
    {
        inCar = true;
    }

    public void Exit()
    {
        inCar = false;
    }

    public void Steer(float amount)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {        
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(new Vector3(0, (-speed*5), 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, (speed*5), 0));
            }
        }
    }

    public void Accelerate(float amount)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * (speed/5));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * (speed/5));
        }
    }

    public Transform GetVehicleExitPoint()
    {
        return exitPoint;
    }

    public bool canEnter()
    {
        if (inCar)
            return false;
        else
            return true;

    }
}
