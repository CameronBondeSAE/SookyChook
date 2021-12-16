using System;
using System.Collections;
using System.Collections.Generic;
using Aaron;
using Tom;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum ProductType
{
    Chicken,
    Egg,
    Rooster
}

public class ChickenGrowingMode : GameModeBase
{
    [Serializable]
    public class Order
    {
        public ProductType productType;
        public int amount;
        public int sellPrice;
    }

    public OrderPoint orderPoint;
    public GameObject shop;

    public List<Order> possibleOrders;
    public List<Order> currentOrders;

    public event Action<Order> NewOrderEvent;
    public event Action<Order> OrderCompleteEvent;
    
    [Tooltip("Time between orders in real-time seconds, NOTE: Does not account for DayNightManager time dilation")]
    public Vector2 orderDelayRange = new Vector2(10, 15);

    [Tooltip("Number of orders that causes a game over")]
    public int maxOrders = 10;

    private Coroutine acceptingOrders;

    public override void Activate()
    {
        base.Activate();
        DayNightManager.Instance.PhaseChangeEvent += SetAcceptingOrders;
        ChickenManager.Instance.ChickenDeathEvent += ChickenCheck;
        orderPoint.OrderPointEvent += CompleteOrder;
        shop.SetActive(true);
    }
    
    public void ChickenCheck()
    {
        if (ChickenManager.Instance.chickensList.Count <= 0)
        {
            MessagesManager.Instance.Show("All your chickens are DEAD!");
            EndMode();
        }
    }

    public void OrderCheck()
    {
        if (currentOrders.Count >= maxOrders)
        {
            MessagesManager.Instance.Show("You didn't fulfil enough orders!");
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

    public void CompleteOrder(CharacterModel player)
    {
        if (player.holdingObject != null)
        {
            GameObject heldObject = (player.holdingObject as MonoBehaviour).gameObject;

            if (heldObject.GetComponent<ISellable>() == null)
            {
                MessagesManager.Instance.Show("You can't sell that!");
                return;
            }
            
            //HACK: Hard-coded to prevent player selling alive chickens
            if (heldObject.GetComponent<ChickenModel>() && heldObject.GetComponent<Health>().isAlive)
            {
                MessagesManager.Instance.Show("You need to KILL that poor chicken!");
                return;
            }

            foreach (Order order in currentOrders)
            {
                if (heldObject.GetComponent<ISellable>().GetProductType() == order.productType)
                {
                    OrderCompleteEvent?.Invoke(order);
                    CashManager.Instance.AddMoney(order.sellPrice);
                    player.Drop(false);
                    Destroy(heldObject);
                    currentOrders.Remove(order);
                    return;
                }
            }
            
            MessagesManager.Instance.Show("I don't want that!");
        }
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
            acceptingOrders = StartCoroutine(AcceptOrders());
        }
        if (phase == DayNightManager.DayPhase.Evening)
        {
            StopCoroutine(acceptingOrders);
        }
    }

    public override void EndMode()
    {
        base.EndMode();

        if (acceptingOrders != null) StopCoroutine(acceptingOrders);
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}