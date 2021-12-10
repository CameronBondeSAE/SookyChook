using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Rob;
using Unity.VisualScripting;
using UnityEngine;


public class TassieSense : MonoBehaviour, ISense
{
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
        isMoving = 9,
        atTarget = 10,
        isAtFarm = 11,
        hasChicken = 12
    }

    public TassieDevilModel tassieDevilModel;
    private FOV tassieFOV;


    private void Start()
    {
        tassieFOV = GetComponent<FOV>();
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(TassieDevilPlanner.seeChicken, SeeChicken());
        aWorldState.Set(TassieDevilPlanner.seeRooster, false);
        aWorldState.Set(TassieDevilPlanner.pathClear, false);
        aWorldState.Set(TassieDevilPlanner.devilNear, false);
        aWorldState.Set(TassieDevilPlanner.seePlayer, false);
        aWorldState.Set(TassieDevilPlanner.returnHome, ReturnHome());
        aWorldState.Set(TassieDevilPlanner.isLooking, aAgent.GetComponent<TassieDevilModel>().isLooking);
        aWorldState.Set(TassieDevilPlanner.fightingDevil, false);
        aWorldState.Set(TassieDevilPlanner.isHungry, aAgent.GetComponent<TassieDevilModel>().isHungry);
        aWorldState.Set(TassieDevilPlanner.isMoving, IsMoving());
        aWorldState.Set(TassieDevilPlanner.atTarget, false);
        aWorldState.Set(TassieDevilPlanner.isAtFarm, IsAtFarm());
        aWorldState.Set(TassieDevilPlanner.hasChicken, aAgent.GetComponent<TassieDevilModel>().hasChicken);
    }

    

    private bool SeeChicken()
    {
        if (tassieDevilModel.prey != null)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        return true;
    }

    private bool ReturnHome()
    {
        if (tassieDevilModel.hasChicken)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        if (tassieDevilModel.prey != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool IsAtFarm()
    {
        if (tassieDevilModel.atTarget)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}