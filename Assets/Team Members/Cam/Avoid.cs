using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
	public RaycastSweep raycastSweep;
	[SerializeField]
	private float multiplier = 1f;
	Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		raycastSweep.Rescan();

		float steeringForce = 0;
		
		foreach (RaycastHit hit in raycastSweep.hits)
		{
			// hit.point

			// Vector3 localPositionOfHit = transform.TransformPoint(hit.point);
			float angle = Vector3.Angle(transform.forward, hit.point);
			Debug.Log(angle);

			steeringForce += (raycastSweep.distance - hit.distance) * ((raycastSweep.spread * raycastSweep.numberOfRays) - angle) * multiplier;
		}
		
		rb.AddRelativeTorque(0,steeringForce,0, ForceMode.Acceleration);		
	}
}
