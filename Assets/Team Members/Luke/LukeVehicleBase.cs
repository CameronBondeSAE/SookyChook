using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LukeVehicleBase : MonoBehaviour, IDrivable
{
	/*
	public PlayerWeek6 player;
	*/
	public List<WheelWeek6> wheels;
	public Transform exitTransform;

	public int playersInside = 0;
	public int maxPlayerCapacity = 1;
	public bool isMaxPlayerCapacity = false;
	
	public float acceleratingForce;
	public float brakingForce;
	public float reversingForce;
	public float maxWheelRotation;
	public float longitudinalFrictionCoefficient;
	public float lateralFrictionCoefficient;
	public float springCoefficient;
	public float dampingCoefficient;
	public float restingWheelHeight;
	public float wheelExtensionFactor;
	public float maxFullSteerVelocity;
	public float minSteerFactor;
	
	public bool active;

	public enum DrivingModes
	{
		Neutral, Drive, Reverse
	}

	public enum SteeringModes
	{
		Neutral, Left, Right
	}
	
	public DrivingModes drivingMode;
	public SteeringModes steeringMode;

	public void ChangeDriveModes(DrivingModes mode)
	{
		drivingMode = mode;
	}
	
	public void ChangeSteerModes(SteeringModes mode)
	{
		steeringMode = mode;
	}

	public virtual void ResetVehicle()
	{
		transform.position += new Vector3 (0f,5f,0f);
		transform.localRotation = Quaternion.Euler(Vector3.Scale(transform.localEulerAngles, new Vector3 (0f,1f,0f)));
	}

	public virtual void ActivateVehicle()
	{
		/*player.DriveEvent += ChangeDriveModes;
		player.SteerEvent += ChangeSteerModes;
		player.ResetEvent += ResetVehicle;
		player.DeactivateVehicleEvent += DeactivateVehicle;*/
		active = true;
	}
	
	public virtual void DeactivateVehicle()
	{
		/*player.DriveEvent -= ChangeDriveModes;
		player.SteerEvent -= ChangeSteerModes;
		player.ResetEvent -= ResetVehicle;
		player.DeactivateVehicleEvent -= DeactivateVehicle;
		player = null;*/
		active = false;
	}
	
    // Start is called before the first frame update
    public virtual void Start()
    {
	    foreach (var t in wheels)
        	    { 
        		    t.acceleratingForce = acceleratingForce;
        		    t.brakingForce = brakingForce;
        		    t.reversingForce = reversingForce;
        		    t.maxRotation = maxWheelRotation;
        		    t.longitudinalFrictionCoefficient = longitudinalFrictionCoefficient;
        		    t.lateralFrictionCoefficient = lateralFrictionCoefficient;
        		    t.springCoefficient = springCoefficient;
        		    t.dampingCoefficient = dampingCoefficient;
        		    t.restingWheelHeight = restingWheelHeight;
        		    t.wheelExtensionFactor = wheelExtensionFactor;
        		    t.maxFullSteerVelocity = maxFullSteerVelocity;
        		    t.minSteerFactor = minSteerFactor;
        	    }
    }

    // Update is called once per frame
    public virtual void Update()
    {
	    
	    
	    if (!active && drivingMode != DrivingModes.Neutral)
	    {
		    drivingMode = DrivingModes.Neutral;
	    }
    }

    public void Enter()
    {
	    playersInside++;
	    if (playersInside == maxPlayerCapacity)
	    {
		    isMaxPlayerCapacity = true;
	    }

	    active = true;
    }

    public void Exit()
    {
	    playersInside--;
	    isMaxPlayerCapacity = false;
	    active = false;

    }

    public void Steer(float amount)
    {
	    if (amount > 0)
	    {
		    steeringMode = SteeringModes.Right;
	    }
	    else
	    {
		    steeringMode = SteeringModes.Left;
	    }
    }

    public void Accelerate(float amount)
    {
	    if (amount > 0)
	    {
		    drivingMode = DrivingModes.Drive;
	    }
	    else
	    {
		    drivingMode = DrivingModes.Reverse;
	    }
    }

    public Transform GetVehicleExitPoint()
    {

	    return exitTransform;

    }

    public bool canEnter()
    {
	    return !isMaxPlayerCapacity;
    }
}
