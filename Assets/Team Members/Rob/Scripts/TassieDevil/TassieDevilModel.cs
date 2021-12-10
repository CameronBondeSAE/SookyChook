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
    public bool atPrey;
    
    public Transform prey;

    public int hunger;
    public int hungerthreshhold;

    
}