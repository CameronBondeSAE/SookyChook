using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Rob;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoosterSense : MonoBehaviour, ISense
{
    public enum RoosterPlanner
    {
        canSeeEnemy = 0,
        canSeeChicken = 1,
        chickenNearby = 2,
        enemyNearby = 3,
        isHungry = 4,
        foodNearby = 5,
        foundFood = 6
    }

    public Rooster_Model rooster;
    public Transform food;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(RoosterPlanner.canSeeChicken, CanSeeChickens());
        aWorldState.Set(RoosterPlanner.canSeeEnemy, CanSeeEnemy());
        aWorldState.Set(RoosterPlanner.chickenNearby, ChickenNearby());
        aWorldState.Set(RoosterPlanner.enemyNearby, EnemyNearby());
        aWorldState.Set(RoosterPlanner.isHungry, rooster.isHungry);
        aWorldState.Set(RoosterPlanner.foodNearby, FoodNearby());
        aWorldState.Set(RoosterPlanner.foundFood, FoundFood());
        //aWorldState.Set(RoosterPlanner.foundFood, true);

    }

    public bool CanSeeChickens()
    {
        if (rooster.target != null)
        {
            return rooster.target.GetComponent<ChickenModel>();
        }

        return false;
    }

    public bool CanSeeEnemy()
    {
        List<GameObject> enemies = rooster.FindEnemies();
        if (enemies.Count > 0)
        {
            rooster.target = enemies[Random.Range(0, enemies.Count)].transform;
            return true;
        }

        return false;
    }

    public bool ChickenNearby()
    {
        if (rooster.target != null && rooster.target.GetComponent<ChickenModel>())
        {
            return TargetNearby();
        }

        return false;
    }

    public bool EnemyNearby()
    {
        if (rooster.target != null && rooster.target.GetComponent<Allegiances>())
        {
            foreach (Allegiances.Entry entry in rooster.allegiances.allegiance)
            {
                if (entry.type == rooster.target.GetComponent<Allegiances>().whatAmI)
                {
                    if (entry.amount < -0.8f)
                    {
                        return TargetNearby();
                    }
                }
            }
        }

        return false;
    }

    public bool FoundFood()
    {
        if (food != null)
        {
            rooster.target = food;
            return true;
        }

        return false;
    }

    public bool FoodNearby()
    {
        if (rooster.target != null && rooster.target.GetComponent<GrassEdible>())
        {
            return TargetNearby();
        }

        return false;
    }

    public bool TargetNearby()
    {
        if (rooster.target != null)
        {
            return Vector3.Distance(transform.position, rooster.target.position) < 2f;
        }

        return false;
    }
}
