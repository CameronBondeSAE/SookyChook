using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public List<VehicleBase> vehicles;

    private void Start()
    {
        foreach (VehicleBase vehicleBase in vehicles)
        {
            vehicleBase.Activate();
        }
    }
}
