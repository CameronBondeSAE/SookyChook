using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WheelWeek6 : MonoBehaviour
{
	public GameObject car;
	private Rigidbody chassis;
	private LukeVehicleBase carClass;

	public bool canSteer;
	public bool canDrive;
	public Vector3 steerAngle;
	public float springCoefficient;
	public float dampingCoefficient;
	public float restingWheelHeight;
	public float wheelExtensionFactor;
	public bool isRearWheel;

	public float longitudinalFrictionCoefficient;
	public float lateralFrictionCoefficient;
	public float acceleratingForce;
	public float brakingForce;
	public float reversingForce;
	public float maxFullSteerVelocity;
	public float minSteerFactor;
	public float currentRotation;
	public float maxRotation;

	public Vector3 localVelocity;
	public Vector3 origin;
	public Vector3 direction;
	private Ray Ray;
	private RaycastHit RaycastHit;

	private bool CheckGroundContact()
	{
		Debug.DrawRay(Ray.origin, Ray.direction, Color.green);
		if (Physics.Raycast(Ray, out RaycastHit, restingWheelHeight*wheelExtensionFactor))
		{
			//suspension
			chassis.AddForceAtPosition((springCoefficient * (restingWheelHeight - RaycastHit.distance)
			                            - dampingCoefficient * Vector3.Dot(chassis.velocity,
				                            car.transform.TransformDirection(Vector3.up)))
			                           * transform.TransformDirection(Vector3.up), origin, 0);
			//longitudinal friction
			chassis.AddForceAtPosition(longitudinalFrictionCoefficient*chassis.mass*transform.
				TransformDirection(new Vector3 (0, 0, -localVelocity.z/3f)), origin);
			
			
			//lateral friction
			chassis.AddForceAtPosition(lateralFrictionCoefficient*chassis.mass*transform.
				TransformDirection(new Vector3 (-localVelocity.x, 0, 0)), origin);
			return true;
		}
		return false;
	}

	private void ApplyDriveForce()
	{
		switch (carClass.drivingMode) 
		{
			case LukeVehicleBase.DrivingModes.Drive:
				if (Vector3.Dot(chassis.velocity, transform.TransformDirection(Vector3.forward)) < 0)
				{
					chassis.AddForceAtPosition((brakingForce*transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				else
				{
					if (canDrive)
					{
						chassis.AddForceAtPosition((acceleratingForce * transform.TransformDirection(Vector3.forward)), origin, 0);
					}
				}
				break;
			case LukeVehicleBase.DrivingModes.Reverse:
				if (Vector3.Dot(chassis.velocity, transform.TransformDirection(Vector3.forward)) > 0)
				{
					chassis.AddForceAtPosition((-brakingForce*transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				else
				{
					if (canDrive)
					{
						chassis.AddForceAtPosition((-reversingForce * transform.TransformDirection(Vector3.forward)), origin, 0);
					}
				}
				break;
		}
	}

	private void ApplySteering()
	{
		currentRotation = Mathf.Clamp(maxRotation*(1-(1-1/minSteerFactor)*Mathf.Abs(localVelocity.z)/maxFullSteerVelocity),
			maxRotation/minSteerFactor, maxRotation);
		switch (carClass.steeringMode)
		{
			case LukeVehicleBase.SteeringModes.Left:
				steerAngle = new Vector3 (0, -currentRotation, 0);
				break;
			case LukeVehicleBase.SteeringModes.Right:
				steerAngle = new Vector3 (0, currentRotation, 0);
				break;
			case LukeVehicleBase.SteeringModes.Neutral:
				steerAngle = Vector3.zero;
				break;
		}
	}

	private void DoWheelThings()
	{
		origin = transform.position;
		direction = transform.TransformDirection(Vector3.down);
		Ray = new Ray(origin, direction);
		localVelocity = transform.InverseTransformDirection(chassis.velocity);

		if (CheckGroundContact())
		{
			ApplyDriveForce();
			if (canSteer)
			{
				ApplySteering();
				if (isRearWheel)
				{
					transform.localEulerAngles = -steerAngle;
				}
				else
				{
					transform.localEulerAngles = steerAngle;
				}
			}
		}
	}
	
// Start is called before the first frame update
    void Awake()
    {
	    car = transform.parent.gameObject;
	    chassis = GetComponentInParent<Rigidbody>();
	    carClass = GetComponentInParent<LukeVehicleBase>();
	    carClass.wheels.Add(this);
	    // should I even have this, or should I just set the bool in the prefab?
	    if (transform.InverseTransformDirection(transform.position).z < transform.InverseTransformDirection(transform.parent.position).z)
	    {
		    isRearWheel = true;
	    }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	    if (!chassis.isKinematic)
	    {
		    DoWheelThings();
	    }
    }
}
