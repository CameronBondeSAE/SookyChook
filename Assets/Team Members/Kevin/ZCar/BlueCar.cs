

using UnityEngine;

public class BlueCar : VehicleBase
{
  public override void Deactivate()
  {
    base.Deactivate();
  }

  public override void Activate()
  {
    Debug.Log("Im Blue!");
  }
}
