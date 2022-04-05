using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MonoBehaviour
{
    public Rigidbody carPrefabRigidbody;
    
    public KeyCode accelerate;
    public float accelerationSpeed = 5f;

    public KeyCode brake;

    public KeyCode left;

    public KeyCode right;

    public KeyCode boost;

    public GameObject[] frontTyres; 
    public Vector3 localVelocity;

    public bool driverIn;
    public PlayerController playerController;
    
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
        ActivatedCar();
        carPrefabRigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void ActivatedCar()
    {
        playerController.AccelCarEvent += Accelerate;
        playerController.DecelerateCarEvent += Decelerate;
        playerController.LeftCarEvent += Left;
        playerController.RightCarEvent += Right;
    }

    private void DeactivateCar()
    {
        playerController.AccelCarEvent -= Accelerate;
        playerController.DecelerateCarEvent -= Decelerate;
        playerController.LeftCarEvent -= Left;
        playerController.RightCarEvent -= Right;
    }

    private void RedCamera()
    {
        GetComponentInChildren<Camera>().enabled = true;
    }

    private void Accelerate()
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
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position,Vector3.,Color.green);
        localVelocity = transform.InverseTransformDirection(carPrefabRigidbody.velocity);
        
        carPrefabRigidbody.AddRelativeForce(new Vector3(-localVelocity.x,0f,0f));
        
        /*
        //Debug.Log(localVelocity);
        
        /*if (Input.GetKey(accelerate) && driverIn)
        {
            
            carPrefabRigidbody.AddRelativeForce(new Vector3(0f, 0f, accelerationSpeed));
            if (Input.GetKey(accelerate) && Input.GetKey(boost))
            {
           
                carPrefabRigidbody.AddRelativeForce(new Vector3(0f, 0f, accelerationSpeed*2));
            }
        }#1#

        if (Input.GetKey(brake)&& driverIn)
        {
            /*frontTyres[0].transform.localRotation =  Quaternion.Euler(0f,0f,0f);
            frontTyres[1].transform.localRotation =  Quaternion.Euler(0f,0f,0f);#1#
            carPrefabRigidbody.AddRelativeForce(new Vector3(0f, 0f, -5f));
        }
        
        if (Input.GetKey(left)&& driverIn)
        {
            //frontTyres[0].transform.localRotation =  Quaternion.Euler(0f,0f,60f);
            //frontTyres[1].transform.localRotation =  Quaternion.Euler(0f,0f,60f);
            //rightFrontTyre.transform.localRotation = Quaternion.Euler(0f,45f,0f);
            //leftFrontTyre.transform.localRotation = Quaternion.Euler(0f,45f,0f);
            carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f));
        }

        if (Input.GetKey(right)&& driverIn)
        {
            //frontTyres[0].transform.localRotation =  Quaternion.Euler(0f,0f,-60f);
            //frontTyres[1].transform.localRotation =  Quaternion.Euler(0f,0f,-60f);
            //rightFrontTyre.transform.localRotation = Quaternion.Euler(0f,-45f,0f);
            //leftFrontTyre.transform.localRotation = Quaternion.Euler(0f,-45f,0f);
            carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, 5f, 0f));
        }

        /*if (Input.GetKey(left))
        {
            
            carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f));
        }

        if (Input.GetKey(right))
        {
            carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, 5f, 0f));
        }#1#*/
    }
}

