using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class RoosterSense : MonoBehaviour, ISense
{
    public enum RoosterPlanner
    {
        chickenNearby = 0,
        canSeeEnemy = 1,
        enemyInRange = 2,
        canSeeChicken = 3
    }

    public Rooster_Model rooster;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(RoosterPlanner.chickenNearby, ChickenNearby());
        aWorldState.Set(RoosterPlanner.canSeeChicken, CanSeeChickens());
        aWorldState.Set(RoosterPlanner.canSeeEnemy, CanSeeEnemy());
        aWorldState.Set(RoosterPlanner.enemyInRange, EnemyInRange());
    }

    public bool CanSeeChickens()
    {
        return rooster.targetChicken != null;
    }

    public bool ChickenNearby()
    {
        if (rooster.targetChicken == null)
        {
            return false;
        }
        
        return Vector3.Distance(transform.position, rooster.targetChicken.position) < 1.5f;
    }

    public bool CanSeeEnemy()
    {
        // TODO: Make this work for all enemies (i.e. foxes, Tassie Devils)
        return rooster.FindObjects<TassieDevilModel>(rooster.sightRange).Count > 0;
    }

    public bool EnemyInRange()
    {
        if (rooster.targetEnemy != null)
        {
            return Vector3.Distance(transform.position, rooster.targetEnemy.position) < 1.5f;
        }

        return false;
    }
}
