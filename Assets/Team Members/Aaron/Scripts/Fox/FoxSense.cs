using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class FoxSense : MonoBehaviour, ISense
{
    //enums instead of strings
    public enum FoxBools
    {
        isInHidingSpot = 0,
        canSeeChicken = 1,
        canSeePlayer = 2,
        isDawn = 3,
        isVisibleToPlayer = 4,
        eatingChicken = 5,
        chickenGone = 6,
        inRangeOfChicken = 7,
        canSeeHidingSpot = 8,
        isHunting = 9
    }
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        
        aWorldState.Set(FoxBools.isInHidingSpot, false);
        aWorldState.Set(FoxBools.canSeeChicken, aAgent.GetComponent<FoxModel>().canSeeChicken);
        aWorldState.Set(FoxBools.canSeePlayer, false);
        aWorldState.Set(FoxBools.isDawn, false);
        aWorldState.Set(FoxBools.isVisibleToPlayer, false);
        aWorldState.Set(FoxBools.eatingChicken, aAgent.GetComponent<FoxModel>().eatingChicken);
        aWorldState.Set(FoxBools.chickenGone, aAgent.GetComponent<FoxModel>().chickenGone);
        aWorldState.Set(FoxBools.inRangeOfChicken, aAgent.GetComponent<FoxModel>().inRange);
        aWorldState.Set(FoxBools.canSeeHidingSpot, false);
        aWorldState.Set(FoxBools.isHunting, aAgent.GetComponent<FoxModel>().isHunting);

        aWorldState.EndUpdate();
    }
}
