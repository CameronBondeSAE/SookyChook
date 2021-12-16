using UnityEngine;

public interface IVehicle
{
	void Enter();
	void Exit();

	/// <summary>
	/// -1 to 1 float for min max steering angle. Scale at your end
	/// </summary>
	/// <param name="amount"></param>
	void Steer(float amount);

	/// <summary>
	/// -1 to 1 float for min max acceleration. -1 for reverse, 1 for forward. Scale at your end
	/// </summary>
	/// <param name="amount"></param>
	void Accelerate(float amount);

	/// <summary>
	/// You must make a child GO at the point of exit on your vehicle and return it here
	/// </summary>
	/// <returns></returns>
	Transform GetVehicleExitPoint();

	bool canEnter();
}

public interface IInteractable
{
	void Interact();
}

public interface IPickupable
{
	void PickUp();
	void PutDown();
}

public interface IWaterable
{
	void BeingWatered(float amount);
}

public interface ITractorAttachment
{
	void Attach(TractorModel aTractorModel);
	void Detach();
	//Vector3 Offset();
}

public interface ISellable
{
	ProductType GetProductType();
}

public interface IUpgradeable
{
	void Upgrade();
}