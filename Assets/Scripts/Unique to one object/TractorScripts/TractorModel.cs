using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorModel : MonoBehaviour, IVehicle
{
    [Header("Vehicle Attributes")]
    public Rigidbody rb;
    public float speed = 100f;
    public float turnRadius = 50f;

    [Header("Needed References")]
    [Tooltip("Where the player will exit the tractor")]
    public Transform exitPoint;
    [Tooltip("Reference to the parent object of all tractor wheels - used for turning off physics when tractor not in use")]
    public GameObject wheels;
    [Tooltip("This is where attachments will be positioned")]
    public Transform attachmentMount;
    //[SerializeField]
    //float detachOffset = 3f;

    [Header("Ragdoll Physics - when jumping out of tractor")]
    [SerializeField]
    float reductionSpeed = 0.05f;
    [SerializeField]
    float reductionTimer = 0.5f;

    [Header("References Only - Don't Touch")]
    public MonoBehaviour attachment;
    public bool hasAttachment = false;

    //private variables
    bool playerInTractor = false;
    bool preventAttachment = false;
    float acceleration;
    float steering;

    //Attachment Reference
    ITractorAttachment tractorAttachment;

    [Space]
    [Header("Turning Wheels Only")]
    public List<Transform> steeringWheels = new List<Transform>();

    [Header("Driving Wheels Only")]
    public List<Transform> drivingWheels = new List<Transform>();

    [Header("For Reference Only")]
    public float steeringAngle;

    //Events
    public event Action EnterTractorEvent;
    public event Action ExitTractorEvent;

    void Start()
    {
        TurnOffTractor();
    }

    private void Update()
    {
        //Only reduce speed when the player is not in the tractor & the tractor is moving
        if(!playerInTractor)
        {
            if (acceleration > 0)
            {
                StartCoroutine(ReduceSpeed());
            }
        }

        //Once the player has stopped moving and player is not in tractor - lock it back up
        if (rb.velocity.magnitude < 0.1f && !playerInTractor)
        {
            TurnOffTractor();
        }
    }

    //Using a coroutine to reduce speed so the function can pause after each speed reduction - allows the tractor to keep moving before reaching zero
    IEnumerator ReduceSpeed()
    {
        if(acceleration > 0)
        {
            acceleration -= reductionSpeed;
            yield return new WaitForSeconds(reductionTimer);
        }
    }

    void TurnOffTractor()
    {
        wheels.SetActive(false);
        rb.isKinematic = true;
    }


    void FixedUpdate()
    {
        steeringAngle = steering * turnRadius;

        foreach (Transform steeringWheel in steeringWheels)
        {
            steeringWheel.localRotation = Quaternion.Euler(0, steeringAngle, 0);
            rb.AddForceAtPosition(steeringWheel.forward * acceleration * speed, steeringWheel.position, ForceMode.Force);
        }

        foreach (Transform driveWheel in drivingWheels)
        {
            //rb.AddRelativeForce(Input.GetAxis("Vertical") * driveWheel.forward * speed);
            rb.AddForceAtPosition(driveWheel.forward * acceleration * speed, driveWheel.position, ForceMode.Force);
        }
    }

    //Trigger check for tractor attachments
    private void OnTriggerEnter(Collider other)
    {
        if(preventAttachment)
        {
            return;
        }

        //Check if the tractor is colliding with an attachment
        tractorAttachment = other.GetComponent<ITractorAttachment>();
        if (tractorAttachment != null)
        {
            //Only attach if there is not already an attachment
            if(!hasAttachment)
            {                
                Attach();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(tractorAttachment != null & !hasAttachment)
        {
            preventAttachment = false;
        }
    }

    #region Interface Implementation
    public void Enter()
    {
        //Invoke event for all view related functionality
        EnterTractorEvent?.Invoke();

        //Model Functionality
        wheels.SetActive(true);
        rb.isKinematic = false;
        playerInTractor = true;

        //If an attachment has been left on the tractor
        // if (hasAttachment)
        // {
            // tractorAttachment.Attach();
            // Dettach();
        // }
    }

    public void Exit()
    {
        ExitTractorEvent?.Invoke();
        playerInTractor = false;

        // if (hasAttachment)
        // {
            // tractorAttachment.Detach();
        // }


        //Update pathfinding on tractor exit
        GlobalEvents.OnLevelStaticsUpdated(gameObject);
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

    public void Attach()
    {
        // attachment = tractorAttachment as MonoBehaviour;
        // attachment.transform.parent = attachmentMount;
        // attachment.transform.localPosition = tractorAttachment.Offset();
        // attachment.transform.rotation = attachmentMount.rotation;
        tractorAttachment.Attach(this);

        //Prevent anymore attachments
        hasAttachment = true;
        preventAttachment = true;
    }

    public void Detach()
    {
        attachment.transform.parent = null;
        attachment.transform.rotation = transform.rotation;
        tractorAttachment.Detach();
        attachment = null;

        hasAttachment = false;
    }

    public Vector3 Offset()
    {
        return attachmentMount.localPosition;
    }

    #endregion
}
