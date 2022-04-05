using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public List<Kcar> vehicles;

    private void Start()
    {
        foreach (Kcar vehicleBase in vehicles)
        {
            vehicleBase.Activate();
        }
    }
}
