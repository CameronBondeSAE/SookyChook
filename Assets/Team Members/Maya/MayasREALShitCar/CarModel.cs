using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel : MonoBehaviour, IDrivable
{
    public float speed;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * (speed/5));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * (speed/5));
        }
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

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Steer(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void Accelerate(float amount)
    {
        throw new System.NotImplementedException();
    }

    public Transform GetVehicleExitPoint()
    {
        throw new System.NotImplementedException();
    }

    public bool canEnter()
    {
        throw new System.NotImplementedException();
    }
}
