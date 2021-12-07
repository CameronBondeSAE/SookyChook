using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
	public float   speed      = 10f;
	public float   jumpHeight = 5f;
	public Vector2 movementDirection;
	public float   exitVehicleForce = 100f;

	[SerializeField]
	private float turnSpeed = 15f;

	public float interactDistance = 1f;

	[Header("Setup")]
	public Rigidbody rb;

	public Vector3 interactRayOffset = new Vector3(0, 0.5f, 0);
	public float   onGroundDrag      = 5f;

	[Header("Info. Don't edit")]
	public bool onGround = true;

	public bool     inVehicle = false;
	public IVehicle IVehicleReference;
	public Vector3  lookMovementDirection;


	public event Action       JumpEvent;
	public event Action       LandedEvent;
	public event Action<bool> OnGroundEvent;

	public event Action<bool> GetInVehicleEvent;
	public event Action<bool> CryingEvent;

	[Header("Cry Variables")]
	public GameObject grass;

	public float maxDistance = 1.8f;
	public float cryTimer    = 3f;
	public Vector3 heightOffset = new Vector3(0, 0.5f, 0);
	float cryTimerValue;

	[SerializeField]
	GameObject holdingObject;

	[SerializeField]
	Transform holdingMount;

	[SerializeField]
	float throwForce = 3f;

	TrailerModel trailer;
	IPickupable pickupable;

	// Start is called before the first frame update
	public void Jump()
	{
		if (!onGround)
			return;

		rb.drag = 0f;
		rb.AddForce(0, jumpHeight, 0, ForceMode.VelocityChange);
		JumpEvent?.Invoke();
	}

	// void Update()
	// {
	// 	RaycastHit hit;
	// 	Ray        ray = new Ray(transform.position + interactRayOffset, transform.forward);
	//
	// 	Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 2f);
	//
	// 	// if (Physics.Raycast(ray, out hit, interactDistance))
	// 	if (Physics.SphereCast(ray, 0.5f, out hit, interactDistance))
	// 	{
	// 		Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
	// 	}
	// }

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!onGround)
		{
			rb.drag = 0f;
			return;
		}
		else
		{
			rb.drag = onGroundDrag;
		}

		Vector3 movementDirectionFinal = new Vector3(movementDirection.x, 0, movementDirection.y);
		rb.AddForce(movementDirectionFinal * speed, ForceMode.Acceleration);
		lookMovementDirection   = movementDirectionFinal;
		lookMovementDirection.y = 0; // Don't look up and down

		// Don't go crazy if you're not moving at all
		if (lookMovementDirection.magnitude > 0.01f)
		{
			// Don't insta look using transform.LookAt, instead Lerp slowlyish so it looks better
			// This calculates the WORLD SPACE rotation to look at a point FROM 0,0,0
			Quaternion toRotation = Quaternion.LookRotation(lookMovementDirection);
			transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

			// Old line, don't use, too crappy looking
			// transform.LookAt(transform.position + lookMovementDirection);
		}
	}

	public void MoveDirection(Vector2 direction)
	{
		if (inVehicle)
		{
			IVehicleReference.Accelerate(direction.y);
			IVehicleReference.Steer(direction.x);
		}
		else
		{
			movementDirection = direction;
		}
	}

	public void Interact()
	{
		if (inVehicle)
		{
			GetOutOfVehicle();
			return;
		}

		RaycastHit hit = CheckWhatsInFrontOfMe();

		// Vehicles?
		IVehicleReference = hit.collider.gameObject.GetComponentInParent<IVehicle>();
		if (IVehicleReference != null)
		{
			if (!inVehicle)
				GetInVehicle();
		}

		IInteractable interactable = hit.collider.gameObject.GetComponentInParent<IInteractable>();
		if (interactable != null)
		{
			interactable.Interact();
		}
	}


	public void PickUp()
	{
		//Always check if something is in front in case player wants to place entities on a trailer
		RaycastHit hit = CheckWhatsInFrontOfMe();

		if(hit.collider != null)
        {
			if(hit.collider.gameObject.GetComponent<TrailerModel>() != null)
            {
				trailer = hit.collider.gameObject.GetComponent<TrailerModel>();
			}

			if(hit.collider.gameObject.GetComponent<IPickupable>() !=null)
            {
				pickupable = hit.collider.gameObject.GetComponent<IPickupable>();
			}
		}
		else
        {
			trailer = null;
			pickupable = null;
        }

		// Already holding something, so drop it
		if (holdingObject != null && !inVehicle && trailer == null)
		{
			Drop();
			return;
		}

		//Holding something and in front of a trailer, place it on trailer
		if (trailer != null && holdingObject != null)
		{
			//TODO: Might not need this if we use a single point for a spawn area
			for (int i = 0; i < trailer.mounts.Length; i++)
			{
				//TODO: Trailer mounts no longer have child GO - need to do physics check?
				//if (trailer.mounts[i].childCount == 0)
				{
					GameObject currentHoldingObject = holdingObject;
					Drop();
					//Hack: Drop has a throw feature
					currentHoldingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
					currentHoldingObject.transform.parent = null; //trailer.mounts[i];
					trailer.objectsInTrailer.Add(currentHoldingObject);
					currentHoldingObject.transform.position = trailer.mounts[i].transform.position;
					currentHoldingObject.transform.rotation = trailer.mounts[i].rotation;
					currentHoldingObject = null;
					return;
				}
			}
		}

		//Not holding anything but in front of a trailer, grab whatever is on the trailer
		//TODO: NOT WORKING
		else if(trailer != null && holdingObject == null && trailer.objectsInTrailer.Count > 0)
        {
			holdingObject = trailer.objectsInTrailer[0];
			trailer.objectsInTrailer.Remove(holdingObject);
			pickupable.PickUp();
			holdingObject.transform.parent = holdingMount;
			holdingObject.transform.localPosition = Vector3.zero;
			holdingObject.transform.rotation = holdingMount.rotation;
			return;
		}


		// HACK: Hardcoded to tractor. Should be able to use interface
		// Note: Characters only need to tell tractors to DETACH. Tractors auto attach on contact
		TractorModel tractorModel = IVehicleReference as TractorModel;
		if (inVehicle && tractorModel.hasAttachment)
		{
			tractorModel.Detach();
			return;
		}

		// Pickup? NOT in vehicle, NOT holding something already
		//Hack: Bug allowing player to pick up trailer
		if (pickupable != null)
		{
			holdingObject = hit.collider.gameObject;
			pickupable.PickUp();
			holdingObject.transform.parent        = holdingMount;
			holdingObject.transform.localPosition = Vector3.zero;
			holdingObject.transform.rotation      = holdingMount.rotation;
		}
	}

	void Drop()
	{
		holdingObject.transform.parent   = null;
		holdingObject.transform.rotation = transform.rotation;
		holdingObject.GetComponent<IPickupable>().PutDown();
		holdingObject.GetComponent<Rigidbody>().velocity = rb.velocity + transform.forward * throwForce; // Throw it out a little + whatever velocity you had
		holdingObject                                    = null;
	}

	public void GetInVehicle()
	{
		// TODO: This only works if there's one collider on root
		inVehicle                        = true;
		GetComponent<Collider>().enabled = false;
		rb.isKinematic                   = true;
		GetInVehicleEvent?.Invoke(true);


		// Lock me to the vehicle, just so the camera doesn't need to retarget anything. I don't actually need to be a child
		MonoBehaviour vehicleComponent = IVehicleReference as MonoBehaviour;
		transform.parent        = vehicleComponent.transform;
		transform.localPosition = Vector3.zero;

		IVehicleReference.Enter();
	}

	public void GetOutOfVehicle()
	{
		// TODO: This only works if there's one collider on root
		inVehicle                        = false;
		GetComponent<Collider>().enabled = true;
		rb.isKinematic                   = false;
		// transform.position = 
		GetInVehicleEvent?.Invoke(false);

		// UNLock me from the vehicle, just so the camera doesn't need to retarget anything. I don't actually need to be a child
		transform.parent = null;

		// Put player at exit point on vehicle
		transform.position = IVehicleReference.GetVehicleExitPoint().position;
		transform.rotation = IVehicleReference.GetVehicleExitPoint().rotation;

		// HACK: Just make the animation look better, fake a jump!
		rb.drag = 0f;
		rb.AddForce(transform.forward * exitVehicleForce, ForceMode.Acceleration);
		onGround = true;
		Jump();
		onGround = false;

		IVehicleReference.Exit();

		// HACK: TODO: Detect favorite chicken death!
		StopCoroutine(Cry());
		StartCoroutine(Cry());
	}

	public IEnumerator Cry()
	{
		// Start crying
		CryingEvent?.Invoke(true);
		cryTimerValue = cryTimer;

		for(int i = 0; i <= cryTimerValue; i++)
        {
			//Shoot raycast down & store what we hit in hitinfo
			RaycastHit hitinfo;
			hitinfo = new RaycastHit();

			//Using height offset to make sure raycase isn't shooting under ground
			Physics.Raycast(transform.position + heightOffset, -transform.up, out hitinfo, maxDistance, 255, QueryTriggerInteraction.Ignore);

			//if we hit something, spawn grass at that hit position (should check if dirt?)
			if (hitinfo.collider)
			{
				GameObject newGrass = Instantiate(grass, hitinfo.point, Quaternion.identity);
			}

			//This causes the for loop to pause at the end of each loop
			yield return new WaitForSeconds(1f);

		}

		//Only pauses once the entire for loop is completed
		yield return new WaitForSeconds(cryTimer - cryTimerValue);

		//Debug.Log("Stopped crying");
		CryingEvent?.Invoke(false);
	}

	RaycastHit CheckWhatsInFrontOfMe()
	{
		// Check what's in front of me. TODO: Make it scan the area or something less precise
		RaycastHit hit;
		// Ray        ray = new Ray(transform.position + transform.TransformPoint(interactRayOffset), transform.forward);
		// NOTE: TransformPoint I THINK includes the main position, so you don't have to add world position to the final
		Vector3 transformPoint = transform.TransformPoint(interactRayOffset);
		Debug.Log(transformPoint);
		Ray ray = new Ray(transformPoint, transform.forward);

		Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green, 2f);

		// if (Physics.Raycast(ray, out hit, interactDistance))
		Physics.SphereCast(ray, 0.5f, out hit, interactDistance);

		return hit;
	}

	void OnCollisionEnter(Collision other)
	{
		onGround = true;
		LandedEvent?.Invoke();
	}

	void OnCollisionExit(Collision other)
	{
		onGround = false;
		OnGroundEvent?.Invoke(false);
	}

	void OnCollisionStay(Collision other)
	{
		onGround = true;
		OnGroundEvent?.Invoke(true);
	}
}