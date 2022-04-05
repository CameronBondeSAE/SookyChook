using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeliveryTruckModel : MonoBehaviour, IDrivable
{
    [Header ("DeliveryTruck Attributes")]
    public Rigidbody rb;

    public float speed = 2000.0f;
    public Transform exitPoint;

    [Header("Wheel Properties")]
    public GameObject Tires;

    public Transform[] wheelVisuals;
    public List<Transform> drivingWheels;
    public List<Transform> steeringWheels;

    [Header("Move Speed")]
    public float acceleration = 0.0f;
    public float maxSteeringAngle = 45.0f;
    private float steering;
    
    
    [Header("View")]
    public AudioSource audioSource;
    public AudioClip enter;
    public AudioClip running;
    public AudioClip exit;
    public AudioClip[] honks;

    bool playerInVehicle;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Honk()
    {
	    audioSource.clip = honks[Random.Range(0,honks.Length)];
	    audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //mechanical aspect for wheels
        foreach (Transform drivingWheel in drivingWheels)
        {
            //rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            rb.AddForceAtPosition(drivingWheel.transform.forward * acceleration * speed, drivingWheel.position);
        }

        foreach (Transform steeringWheel in steeringWheels)
        {
            //rb.AddForceAtPosition(transform.forward*acceleration*speed,transform.position);
            rb.AddForceAtPosition(steeringWheel.forward*acceleration*speed,steeringWheel.position);
            steeringWheel.localRotation = Quaternion.Euler(0,steering*maxSteeringAngle,0);
        }
        
        //visual aspect for steering wheels
        foreach (Transform v in wheelVisuals)
        {
            v.DOLocalRotateQuaternion(Quaternion.Euler(0, steering * maxSteeringAngle, 0), acceleration);
        }
    }

    private Coroutine co;
    public void Enter()
    {
        Tires.SetActive(true);
        rb.isKinematic = false;
        playerInVehicle = true;
        
        co = StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
	    // HACK: Move View
	    audioSource.clip = enter;
	    audioSource.loop = false;
	    audioSource.Play();
	    yield return new WaitForSeconds(enter.length);
	    audioSource.loop = true;
	    audioSource.clip = running;
	    audioSource.Play();
    }

    public void Exit()
    {
        Tires.SetActive(false);
        playerInVehicle = false;
        
        // HACK: Move View
        // Stops the starting sounds sequence
        StopCoroutine(co);

        audioSource.clip = exit;
        audioSource.loop = false;
        audioSource.Play();
        //Debug.Log("Exited");
    }

    public void Steer(float amount)
    {
        steering = amount;
    }

    public void Accelerate(float amount)
    {
        acceleration = amount;
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
