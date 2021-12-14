using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FenceModel : MonoBehaviour, IPickupable
{
    [Header("Fence Components Reference")]
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Collider[] collider;

    [SerializeField]
    [Header("Strength of fence offset")]
    float  offset = 1f;

    //Fence Events
    public event Action<bool> PickedUpEvent;

    public void PickUp()
    {
        //Send event for all view related functionality
        PickedUpEvent?.Invoke(true);

        //Model Functionality
        foreach(Collider c in collider)
        {
            c.enabled = false;
        }
        rb.useGravity = false;
        rb.isKinematic = true;
        
        //Invoke event to update pathfinding algorithms
        GlobalEvents.OnLevelStaticsUpdated(gameObject);
    }

    public void PutDown()
    {
        PickedUpEvent?.Invoke(false);

        //Reset Model Components
        foreach (Collider c in collider)
        {
            c.enabled = true;
        }
        rb.useGravity = true;
        //rb.isKinematic = false;

        //Raycasting to find where to place fence
        RaycastHit hitinfo;
        hitinfo = new RaycastHit();
        Physics.Raycast(transform.position + (transform.forward * offset), -transform.up, out hitinfo, 2, 255, QueryTriggerInteraction.Ignore);

        //Place fence at raycast hit point
        if (hitinfo.collider)
        {
            //rb.velocity = Vector3.zero;
            transform.position = hitinfo.point;
        }

        //Invoke event to update pathfinding algorithms
        GlobalEvents.OnLevelStaticsUpdated(gameObject);
    }
}
