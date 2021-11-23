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
	public IVehicle vehicle;
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

	[SerializeField]
	GameObject holdingObject;

	[SerializeField]
	Transform holdingMount;

	[SerializeField]
	float throwForce = 3f;

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
			vehicle.Accelerate(direction.y);
			vehicle.Steer(direction.x);
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
		vehicle = hit.collider.gameObject.GetComponent<IVehicle>();
		if (vehicle != null)
		{
			if (!inVehicle)
				GetInVehicle();
		}

		IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
		if (interactable != null)
		{
			interactable.Interact();
		}
	}


	public void PickUp()
	{
		// Already holding something, so drop it
		if (holdingObject != null)
		{
			holdingObject.transform.parent   = null;
			holdingObject.transform.rotation = transform.rotation;
			holdingObject.GetComponent<IPickupable>().PutDown();
			holdingObject.GetComponent<Rigidbody>().velocity = rb.velocity + transform.forward * throwForce; // Throw it out a little + whatever velocity you had
			holdingObject                                    = null;
			return;
		}

		// Pickup?
		RaycastHit  hit        = CheckWhatsInFrontOfMe();
		IPickupable pickupable = hit.collider.gameObject.GetComponent<IPickupable>();

		if (pickupable != null)
		{
			holdingObject = hit.collider.gameObject;
			pickupable.PickUp();
			holdingObject.transform.parent        = holdingMount;
			holdingObject.transform.localPosition = Vector3.zero;
			holdingObject.transform.rotation      = holdingMount.rotation;
		}
	}

	public void GetInVehicle()
	{
		// TODO: This only works if there's one collider on root
		inVehicle                        = true;
		GetComponent<Collider>().enabled = false;
		rb.isKinematic                   = true;
		GetInVehicleEvent?.Invoke(true);


		// Lock me to the vehicle, just so the camera doesn't need to retarget anything. I don't actually need to be a child
		MonoBehaviour vehicleComponent = vehicle as MonoBehaviour;
		transform.parent        = vehicleComponent.transform;
		transform.localPosition = Vector3.zero;

		vehicle.Enter();
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
		transform.position = vehicle.GetVehicleExitPoint().position;
		transform.rotation = vehicle.GetVehicleExitPoint().rotation;

		// HACK: Just make the animation look better, fake a jump!
		rb.drag = 0f;
		rb.AddForce(transform.forward * exitVehicleForce, ForceMode.Acceleration);
		onGround = true;
		Jump();
		onGround = false;

		vehicle.Exit();

		// HACK: TODO: Detect favorite chicken death!
		StopCoroutine(Cry());
		StartCoroutine(Cry());
	}

	public IEnumerator Cry()
	{
		// Start crying
		CryingEvent?.Invoke(true);

		//Shoot raycast down & store what we hit in hitinfo
		RaycastHit hitinfo;
		hitinfo = new RaycastHit();
		Physics.Raycast(transform.position, -transform.up, out hitinfo, maxDistance, 255, QueryTriggerInteraction.Ignore);

		//if we hit something, spawn grass at that hit position (should check if dirt?)
		if (hitinfo.collider)
		{
			GameObject newGrass = Instantiate(grass, hitinfo.point, Quaternion.identity);
		}

		yield return new WaitForSeconds(cryTimer);

		Debug.Log("Stopped crying");
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