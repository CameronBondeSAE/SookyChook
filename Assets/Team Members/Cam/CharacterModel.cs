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

	public bool         inVehicle = false;
	public IVehicleBase vehicleBase;
	public Vector3      lookMovementDirection;

	


	public event Action       JumpEvent;
	public event Action       LandedEvent;
	public event Action<bool> OnGroundEvent;

	public event Action<bool> GetInVehicleEvent;


	// Start is called before the first frame update
	public void Jump()
	{
		if (!onGround)
			return;

		rb.drag = 0f;
		rb.AddForce(0, jumpHeight, 0, ForceMode.VelocityChange);
		JumpEvent?.Invoke();
	}

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

	public void Interact()
	{
		if (inVehicle)
		{
			GetOutOfVehicle();
			return;
		}

		// Check what's in front of me. TODO: Make it scan the area or something less precise
		RaycastHit hit;
		Ray        ray = new Ray(transform.position + interactRayOffset, transform.forward);

		Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green, 2f);

		if (Physics.Raycast(ray, out hit, interactDistance))
		{
			Collider hitCollider = hit.collider;

			// Vehicles?
			vehicleBase = hitCollider?.gameObject.GetComponent<IVehicleBase>();
			if (vehicleBase != null)
			{
				if (!inVehicle)
					GetInVehicle();
			}
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
		MonoBehaviour vehicleComponent = vehicleBase as MonoBehaviour;
		transform.parent        = vehicleComponent.transform;
		transform.localPosition = Vector3.zero;

		vehicleBase.Enter();
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
		transform.position = vehicleBase.GetVehicleExitPoint().position;
		transform.rotation = vehicleBase.GetVehicleExitPoint().rotation;

		// HACK: Just make the animation look better, fake a jump!
		rb.drag = 0f;
		rb.AddForce(transform.forward * exitVehicleForce, ForceMode.Acceleration);
		onGround = true;
		Jump();

		vehicleBase.Exit();
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