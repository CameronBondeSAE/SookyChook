using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorModel : MonoBehaviour, IVehicle, ITractorAttachment
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
    public GameObject attachment;
    public bool hasAttachment = false;

    //private variables
    bool playerInTractor = false;
    float acceleration;
    float steering;

    //Attachment References
    ITractorAttachment tractorAttachment;
    MonoBehaviour currentAttachment;

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
        wheels.SetActive(false);
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
            wheels.SetActive(false);
            rb.isKinematic = true;
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

    private void OnTriggerEnter(Collider other)
    {
        //Check if the tractor is colliding with an attachment
        tractorAttachment = other.GetComponent<ITractorAttachment>();
        if (tractorAttachment != null)
        {
            currentAttachment = tractorAttachment as MonoBehaviour;

            //Only attach if there is not already an attachment
            if(!hasAttachment)
            {
                Attach();
            }
        }
    }

    public void Enter()
    {
        //Invoke event for all view related functionality
        EnterTractorEvent?.Invoke();

        //Model Functionality
        wheels.SetActive(true);
        rb.isKinematic = false;
        playerInTractor = true;

        //If an attachment has been left on the tractor
        if (hasAttachment)
        {
            currentAttachment.GetComponent<SeedPlanterModel>().Attach();
            //Dettach();
        }
    }

    public void Exit()
    {
        ExitTractorEvent?.Invoke();
        playerInTractor = false;

        if (hasAttachment)
        {
            currentAttachment.GetComponent<SeedPlanterModel>().Dettach();
            //Dettach();
        }


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
        attachment = currentAttachment.gameObject;
        attachment.transform.parent = attachmentMount;
        attachment.transform.localPosition = new Vector3(0, 0, 1f);
        attachment.transform.rotation = attachmentMount.rotation;
        tractorAttachment.Attach();

        hasAttachment = true;
    }

    public void Dettach()
    {
        attachment.transform.parent = null;
        attachment.transform.rotation = transform.rotation;
        currentAttachment.transform.position = attachment.transform.position;
        //attachment.transform.position = transform.localPosition + (-transform.forward * detachOffset);
        currentAttachment.GetComponent<SeedPlanterModel>().Dettach();
        attachment = null;

        Invoke("ResetAttachment", 2f);
    }

    //Hack for now to prevent attachment being instantly reattached to the tractor
    private void ResetAttachment()
    {
        hasAttachment = false;
    }
}
