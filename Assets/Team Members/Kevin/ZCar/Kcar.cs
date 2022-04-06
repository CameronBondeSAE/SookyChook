using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kcar : MonoBehaviour, IDrivable
{
    public Rigidbody carPrefabRigidbody;
    
    public float accelerationSpeed = 5f;

    public GameObject kCar; 
    public GameObject[] frontTyres; 
    public Vector3 localVelocity;

    public bool driverIn;
    public PlayerController playerController;
    
    public Rigidbody rb; 
    public Transform exitPoint;

    public bool drive;
    public bool brake;
    public bool left;
    public bool right; 
    public virtual void Activate()
    {
        Debug.Log("Activated");
    }

    public virtual void Deactivate()
    {
        Debug.Log("Deactivated");
    }
    
    public void OnEnable()
    {
        //PlayerController += Accelerate;
    }
    
    void Start()
    {
        //ActivatedCar();
        carPrefabRigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void ActivatedCar()
    {
        /*playerController.AccelCarEvent += Accelerate;
        playerController.DecelerateCarEvent += Decelerate;
        playerController.LeftCarEvent += Left;
        playerController.RightCarEvent += Right;*/
    }

    private void DeactivateCar()
    {
        /*playerController.AccelCarEvent -= Accelerate;
        playerController.DecelerateCarEvent -= Decelerate;
        playerController.LeftCarEvent -= Left;
        playerController.RightCarEvent -= Right;*/
    }

    /*
    private void RedCamera()
    {
        GetComponentInChildren<Camera>().enabled = true;
    }*/

    /*private void Accelerate()
    {
        if (driverIn)
        {
            carPrefabRigidbody.AddRelativeForce(new Vector3(0f, 0f, accelerationSpeed*2));
        }
    }

    private void Decelerate()
    {
        if (driverIn)
        {
            carPrefabRigidbody.AddRelativeForce(new Vector3(0f, 0f, -5f));
        }
        
    }

    private void Left()
    {
        if (driverIn)
        {
            carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f));
        }
        
    }

    private void Right()
    {
        if (driverIn)
        {
            carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, 5f, 0f));
        }
        
    }*/
    

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position,Vector3.,Color.green);
        localVelocity = transform.InverseTransformDirection(carPrefabRigidbody.velocity);
        
        carPrefabRigidbody.AddRelativeForce(new Vector3(-localVelocity.x,0f,0f));
    }

    void FixedUpdate()
    {
        if (drive)
        {
            Drive();
        }

        if (brake)
        {
            Brake();
        }

        if (left)
        {
            Left();
        }

        if (right)
        {
            Right();
        }
    }

    void Drive()
    {
        carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f)); 
    }

    void Brake()
    {
        carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, 5f, 0f));
    }

    void Left()
    {
        carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f)); 
    }

    void Right()
    {
        carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, 5f, 0f));
    }
    
    public void Enter()
    {
        driverIn = true;
        kCar.SetActive(true);
        rb.isKinematic = false;
    }

    public void Exit()
    {
        driverIn = false;
        //kCar.SetActive(false);
    }
    
    public void Steer(float amount)
    {
        if (amount > 0 && driverIn && drive)
        {
            left = true;
            right = false;
        }
        else
        {
            left = false;
            right = true;
        }
    }

    public void Accelerate(float amount)
    {
        if (amount > 0 && driverIn)
        {
            drive = true;
            brake = false;
        }
        else
        {
            drive = false;
            brake = true;
        }
        
       
    }

    public Transform GetVehicleExitPoint()
    {
        return exitPoint; 
    }

    public bool canEnter()
    {
        if(driverIn)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

