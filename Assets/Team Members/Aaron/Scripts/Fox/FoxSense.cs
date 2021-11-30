using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class FoxSense : MonoBehaviour, ISense
{
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set("isNight", false);
        aWorldState.Set("isInHidingSpot", false);
        aWorldState.Set("canSeeChicken", false);
        aWorldState.Set("canSeePlayer", false);
        aWorldState.Set("isSDawn", false);
        aWorldState.Set("isVisibleToPlayer", false);
        aWorldState.Set("gotChicken", false);
        aWorldState.Set("chickenDead", false);
        aWorldState.Set("inRangeOfChicken", false);
        aWorldState.Set("canSeeHidingSpot", false);

        aWorldState.EndUpdate();
    }
}
