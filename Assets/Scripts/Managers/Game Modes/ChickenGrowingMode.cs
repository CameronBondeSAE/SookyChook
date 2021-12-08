using System;
using System.Collections;
using System.Collections.Generic;
using Aaron;
using Tom;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ProductType
{
    Chicken,
    Egg,
    Rooster
}

public class ChickenGrowingMode : GameModeBase
{
    public List<CharacterModel> players;
    public Transform[] playerSpawns = new Transform[4];

    //public OrderDropoffZone orderZone;

    [Serializable]
    public class Order
    {
        public ProductType productType;
        public int amount;
    }

    public List<Order> possibleOrders;

    public event Action<Order> NewOrderEvent;

    public override void Activate()
    {
        base.Activate();

        for (int i = 0; i < players.Count; i++)
        {
            if (playerSpawns[i] != null)
            {
                Instantiate(players[i], playerSpawns[i].position, playerSpawns[i].rotation);
            }
        }
        
        NewOrder();

        // DayNightManager.Instance.PhaseChangeEvent += OrderCheck;
        // ChickenManagerEvent += ChickenCheck;
    }
    
    
    public void ChickenCheck()
    {
        if (ChickenManager.Instance.chickensList.Count <= 0)
        {
            EndMode();
        }
    }

    // public void OrderCheck(DayNightManager.DayPhase phase)
    // {
    //     if (phase == DayNightManager.DayPhase.Evening && !orderZone.orderCompleted)
    //     {
    //         EndMode();
    //     }
    // }

    public void NewOrder()
    {
        Order newOrder = possibleOrders[Random.Range(0, possibleOrders.Count)];
        NewOrderEvent?.Invoke(newOrder);
    }
}