using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Tyres : MonoBehaviour
{
    public float rayMaxDistance;

    public GameObject carObject;

    public Vector3 force;

    public AnimationCurve animationCurve;

    public Vector3 localVelocity;

    public Rigidbody chassis;

    public Vector3 origin; 
    //public float suspensionHeight; 
    // Start is called before the first frame update
    void Start()
    {
        //chassis = GetComponent<SceneManager>().chassis;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        /*localVelocity = transform.InverseTransformDirection(GetComponent<Body>().carPrefabRigidbody.velocity);
        GetComponent<Body>().carPrefabRigidbody.AddRelativeForce(new Vector3(-localVelocity.x,0f,0f));*/
        
        
        Ray ray = new Ray();
        ray.direction = transform.TransformDirection(Vector3.down);
        ray.origin = gameObject.transform.position;
        RaycastHit raycastHit = new RaycastHit();
        force = new Vector3(0f, rayMaxDistance, 0f);
        //suspensionHeight = raycastHit.distance; 
        if (Physics.Raycast(ray, out raycastHit, rayMaxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            carObject.GetComponent<Rigidbody>().AddForceAtPosition(force, ray.origin);
            if (raycastHit.distance > 0.5f)
            {
                carObject.GetComponent<Rigidbody>().AddForceAtPosition(-force, ray.origin);
            }

            if (raycastHit.distance <= 0.49f)
            {
                carObject.GetComponent<Rigidbody>().AddForceAtPosition(force, ray.origin);
            }
            //carObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f,-rayMaxDistance,0f));
            //Debug.Log(raycastHit.distance); 
        }
        
        /*chassis = chassis.GetComponent<SceneManager>().chassis;
        origin = transform.localPosition;
        localVelocity = transform.InverseTransformDirection(chassis.velocity);
        
        chassis.AddForceAtPosition(chassis.mass*transform.TransformDirection(new Vector3 (-localVelocity.x,0f,0f)),origin);
        lateralFrictionCoefficient**/
            
        /*if (Input.GetKey(left))
        {
            //rightFrontTyre.transform.localRotation = Quaternion.Euler(0f,45f,0f);
            //leftFrontTyre.transform.localRotation = Quaternion.Euler(0f,45f,0f);
            GetComponent<Body>().carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f));
        }

        if (Input.GetKey(right))
        {
            //rightFrontTyre.transform.localRotation = Quaternion.Euler(0f,-45f,0f);
            //leftFrontTyre.transform.localRotation = Quaternion.Euler(0f,-45f,0f);
            GetComponent<Body>().carPrefabRigidbody.AddRelativeTorque(new Vector3(0f, -5f, 0f));
        }
        /*
        if (raycastHit.distance > 0.5f)
        {
            carObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f,-rayMaxDistance,0f));
        }

        if (raycastHit.distance <= 0.5f)
        {
            carObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f,rayMaxDistance,0f));
        }
        #1#*/

    }
}
