using System;
using System.Collections;
using System.Collections.Generic;
using Rob;
using UnityEditor;
using UnityEngine;

public class DeliverySequence : MonoBehaviour
{
    [Header("Variable Changes")]
    public Spawner spawner;
    public GameObject deliveryTruck;
    public Transform deliveryTruckPos;
    public float deliveryTime = 11.8f;
    public float truckSpeed = 1.0f;
    
    [Header("ReadOnly")]
    public float deliveryEvent;
    
    void Start()
    {
        DayNightManager.Instance.PhaseChangeEvent += InstanceOnPhaseChangeEvent;
        
        //Instantiate(deliveryTruck, transform.position, Quaternion.Euler(0,-90,0));
    }

    private void InstanceOnPhaseChangeEvent(DayNightManager.DayPhase obj)
    {
        if (obj == DayNightManager.DayPhase.Noon)
        {
            //TODO: Need to make truck move back and forward consistently. Need to orient position of truck
            //deliveryTruck.transform.position=new Vector3();
            //Vector3(deliveryTruckPos);
            //deliveryTruck
            
            deliveryTruck.isStatic = false;
            deliveryTruck.SetActive(true);
            //Instantiate(deliveryTruck, transform.localPosition, Quaternion.identity);
            StartCoroutine(Delivery(new float()));
        }
    }

    public IEnumerator Delivery(float amount)
    {
        //Reverse
        FindObjectOfType<DeliveryTruckModel>().Accelerate(-truckSpeed);
        Debug.Log("Back");
        yield return new WaitForSeconds(2.0f);
        
        //Accelerate
        FindObjectOfType<DeliveryTruckModel>().Accelerate(truckSpeed);
        Debug.Log("Forward");
        yield return new WaitForSeconds(3.5f);
        
        deliveryTruck.SetActive(false);
        deliveryTruck.isStatic = true;
        //TODO: remove box collider to make fences splurge out, also adjust spawn radius of fences in prefab.
        StopCoroutine(Delivery(new float()));
    }
}
