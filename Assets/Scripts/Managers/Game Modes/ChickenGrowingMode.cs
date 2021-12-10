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
    //public OrderDropoffZone orderZone;

    [Serializable]
    public class Order
    {
        public ProductType productType;
        public int amount;
    }

    public List<Order> possibleOrders;
    public List<Order> currentOrders;

    public event Action<Order> NewOrderEvent;
    
    [Tooltip("Time between orders in real-time seconds, NOTE: Does not account for DayNightManager time dilation")]
    public Vector2 orderDelayRange = new Vector2(10, 15);

    [Tooltip("Number of orders that causes a game over")]
    public int maxOrders = 10;

    public override void Activate()
    {
        base.Activate();
        
        DayNightManager.Instance.PhaseChangeEvent += SetAcceptingOrders;
        ChickenManager.Instance.ChickenDeathEvent += ChickenCheck;
    }
    
    public void ChickenCheck()
    {
        if (ChickenManager.Instance.chickensList.Count <= 0)
        {
            EndMode();
        }
    }

    public void OrderCheck()
    {
        if (currentOrders.Count >= maxOrders)
        {
            EndMode();
        }
    }

    public void NewOrder()
    {
        Order newOrder = possibleOrders[Random.Range(0, possibleOrders.Count)];
        currentOrders.Add(newOrder);
        NewOrderEvent?.Invoke(newOrder);
        OrderCheck();
    }

    public IEnumerator AcceptOrders()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(orderDelayRange.x, orderDelayRange.y));
            NewOrder();
        }
    }

    public void SetAcceptingOrders(DayNightManager.DayPhase phase)
    {
        if (phase == DayNightManager.DayPhase.Morning)
        {
            StartCoroutine(AcceptOrders());
        }
        if (phase == DayNightManager.DayPhase.Evening)
        {
            StopCoroutine(AcceptOrders());
        }
    }
}