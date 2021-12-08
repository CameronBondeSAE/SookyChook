using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class FoxSense : MonoBehaviour, ISense
{
    //enums instead of strings
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        
        aWorldState.Set("isInHidingSpot", false);
        aWorldState.Set("canSeeChicken", aAgent.GetComponent<FoxModel>().canSeeChicken);
        aWorldState.Set("canSeePlayer", false);
        aWorldState.Set("isSDawn", false);
        aWorldState.Set("isVisibleToPlayer", false);
        aWorldState.Set("eatingChicken", aAgent.GetComponent<FoxModel>().eatingChicken);
        aWorldState.Set("chickenGone", aAgent.GetComponent<FoxModel>().chickenGone);
        aWorldState.Set("inRangeOfChicken", aAgent.GetComponent<FoxModel>().inRange);
        aWorldState.Set("canSeeHidingSpot", false);
        aWorldState.Set("isHunting", aAgent.GetComponent<FoxModel>().isHunting);

        aWorldState.EndUpdate();
    }
}
