using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Events;

public class ChickenModel : MonoBehaviour, IInteractable, IPickupable
{
    public float hungerLevel;
    public float hungerThreshold = 0.5f;
    public bool  isHungry;
    
    //public float growth;

    public bool   isFull;
    public Edible targetEdible;

    public event Action InteractEvent;
    public event Action<bool> PickUpEvent;

    // Start is called before the first frame update
    void Start()
    {
        hungerLevel = 0.5f;
        StartCoroutine("ReduceHungerTime");
    }

    // Update is called once per frame
    void Update()
    {
       //if eats food, increase scale size by growth until localScale == 1
    }
    
    private IEnumerator ReduceHungerTime()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        
        ReduceHunger(0.1f);
        StartCoroutine("ReduceHungerTime");
    }
    
    public void ReduceHunger(float reduction)
    {
        hungerLevel -= reduction;

        if (hungerLevel > hungerThreshold)
        {
            isHungry = true;
        }
    }

    #region Interface implementation
    
    public void Interact()
    {
        InteractEvent?.Invoke();
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled      = false;
        PickUpEvent?.Invoke(true);
    }

    public void PutDown()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled      = true;
        PickUpEvent?.Invoke(false);
    }
    
    #endregion
}
