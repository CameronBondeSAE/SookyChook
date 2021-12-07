using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChickenSense : MonoBehaviour, ISense
{
    public enum Conditions
    {
        isHungry  = 0,
        isScared  = 1,
        isDusk    = 2,
        isDawn    = 3,
        foundFood = 4,
        atFood    = 5
    }

    public ChickenModel chickenModel;
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(Conditions.isHungry, chickenModel.isHungry);
        aWorldState.Set(Conditions.isScared, false);
        aWorldState.Set(Conditions.isDusk, false);
        aWorldState.Set(Conditions.isDawn, false);
        aWorldState.Set(Conditions.foundFood, false);
        aWorldState.Set(Conditions.atFood, false);
    }
}
