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
        atPrey = 10
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
        aWorldState.Set(TassieDevilPlanner.isLooking, true);
        aWorldState.Set(TassieDevilPlanner.fightingDevil, false);
        aWorldState.Set(TassieDevilPlanner.isHungry, aAgent.GetComponent<TassieDevilModel>().isHungry);
        aWorldState.Set(TassieDevilPlanner.isMoving,IsMoving());
        aWorldState.Set(TassieDevilPlanner.atPrey,false);
        
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
        throw new System.NotImplementedException();
    }

    private bool ReturnHome()
    {
        if (tassieDevilModel.atPrey)
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
        return true;
    }
}