using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Events;

public class ChickenModel : MonoBehaviour
{
    public int maxHunger;
    public float hungerLevel;
    //public float growth;

    public bool isFull;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHunger = 10;
        hungerLevel = maxHunger / 2;
        StartCoroutine("ReduceHungerTime");
    }

    // Update is called once per frame
    void Update()
    {
       //if eats food, increase scale size by growth until localScale == 1
    }
    
    //HONESTLY PROBS A HACK, PLEASE SHOW ME ANOTHER CLEANER WAY TO DO THIS!
    private IEnumerator ReduceHungerTime()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
        }
        
        ReduceHunger(0.1f);
        //restarts the coroutine/timer thing. Need a new way to so this
        StartCoroutine("ReduceHungerTime");
    }
    
    public void ReduceHunger(float reduction)
    {
        hungerLevel -= reduction;
    }
}
