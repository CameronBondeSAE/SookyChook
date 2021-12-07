using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;


public class TassieSense : MonoBehaviour, ISense
{
    public enum TassieDevilPlanner
    {
        seeChicken = 0,
        seeRosster = 1,
        pathClear = 2,
        devilNear = 3,
        seePlayer = 4,
        returnHome = 5,
        isLooking = 6,
        fightingDevil = 7,
        isHungry = 8
    }

    public TassieDevilModel tassieDevilModel;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(TassieDevilPlanner.seeChicken, SeeChicken());
        aWorldState.Set(TassieDevilPlanner.seeRosster, SeeRooster());
        aWorldState.Set(TassieDevilPlanner.pathClear, PathClear());
        aWorldState.Set(TassieDevilPlanner.devilNear, DevilIsNear());
        aWorldState.Set(TassieDevilPlanner.seePlayer, SeePlayer());
        aWorldState.Set(TassieDevilPlanner.returnHome, false);
        aWorldState.Set(TassieDevilPlanner.isLooking, false);
        aWorldState.Set(TassieDevilPlanner.fightingDevil, FightingDevil());
        aWorldState.Set(TassieDevilPlanner.isHungry, IsHungry());
    }

    private bool SeeChicken()
    {
        throw new System.NotImplementedException();
    }

    private bool SeeRooster()
    {
        throw new System.NotImplementedException();
    }

    private bool PathClear()
    {
        throw new System.NotImplementedException();
    }

    private bool DevilIsNear()
    {
        throw new System.NotImplementedException();
    }

    private bool SeePlayer()
    {
        throw new System.NotImplementedException();
    }

    private bool FightingDevil()
    {
        throw new System.NotImplementedException();
    }

    private bool IsHungry()
    {
        //if hunger > min hunger
        return true;
    }
}