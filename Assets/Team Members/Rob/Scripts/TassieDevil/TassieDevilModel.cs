using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class TassieDevilModel : MonoBehaviour
{
    public AntAIAgent aiAgent;
    
    
    public bool seeChicken;
    public bool isHungry;
    public bool isMoving;
    public bool isLooking = true;
    public bool atTarget;
    public bool hasChicken;
    public bool isAtFarm;
    public bool atPrey;
    public bool returnHome;

    public Transform prey;

    public int hunger;
    public int hungerthreshhold;
    public int maxHunger;


    private void Start()
    {
        hunger = maxHunger;
        StartCoroutine(DecreaseHunger());
    }

    private IEnumerator DecreaseHunger()
    {
        yield return new WaitForSeconds(1f);
        hunger -= 1;
        if (hunger <= 0)
        {
            hunger = 0;
        }
        StartCoroutine(DecreaseHunger());
    }

    private void Update()
    {
        if (hunger <= hungerthreshhold)
        {
            isHungry = true;
        }
    }
}