using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;


public class TassieSense : MonoBehaviour, ISense
{
    // public enum TassieDevilPlanner
    // {
    //     seeChicken = 0,
    //     seeRosster = 1,
    //     pathClear = 2,
    //     devilNear = 3,
    //     seePlayer = 4,
    //     returnHome = 5,
    //     isLooking = 6,
    //     fightingDevil = 7,
    //     isHungry = 8
    // }
    public enum TassieDevilPlanner
    {
        seeChicken = 0,
        seeRooster = 1,
        pathClear = 2,
        devilNear = 3,
        seePlayer = 4,
        returnHome = 5,
        isLooking = 6,
        fightingDevil = 7,
        isHungry = 8,
        isMoving = 9
    }

    public TassieDevilModel tassieDevilModel;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        
        aWorldState.Set(TassieDevilPlanner.seeChicken, aAgent.GetComponent<TassieDevilModel>().seeChicken);
        aWorldState.Set(TassieDevilPlanner.seeRooster, SeeRooster());
        aWorldState.Set(TassieDevilPlanner.pathClear, PathClear());
        aWorldState.Set(TassieDevilPlanner.devilNear, DevilIsNear());
        aWorldState.Set(TassieDevilPlanner.seePlayer, SeePlayer());
        aWorldState.Set(TassieDevilPlanner.returnHome, false);
        aWorldState.Set(TassieDevilPlanner.isLooking, true);
        aWorldState.Set(TassieDevilPlanner.fightingDevil, FightingDevil());
        aWorldState.Set(TassieDevilPlanner.isHungry, aAgent.GetComponent<TassieDevilModel>().isHungry);
        aWorldState.Set(TassieDevilPlanner.isMoving, aAgent.GetComponent<TassieDevilModel>().isMoving);
        
        aWorldState.EndUpdate();
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
    
    private bool IsMoving()
    {
        throw new System.NotImplementedException();
    }
}