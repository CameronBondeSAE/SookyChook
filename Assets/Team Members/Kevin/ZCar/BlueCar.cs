

using UnityEngine;

public class BlueCar : Kcar
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
