using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tom
{
    [RequireComponent(typeof(BoxCollider))]
    public class OrderDropoffZone : MonoBehaviour
    {
        [Serializable]
        public class Order
        {
            public GameObject objectType;
            public int amount;
        }

        public List<Order> possibleOrders = new List<Order>();
        public Order currentOrder;
        public bool orderCompleted = false;
        public DayNightManager.DayPhase newOrderPhase = DayNightManager.DayPhase.Morning;
        private BoxCollider triggerBox;

        public void Start()
        {
            triggerBox = GetComponent<BoxCollider>();
            DayNightManager.Instance.PhaseChangeEvent += SetOrder;
        }

        public void OnTriggerEnter(Collider other)
        {
            CharacterModel character = other.GetComponent<CharacterModel>();
            if (character != null)
            {
                //GameObject characterObject = character.holdingObject.gameObject;
                //if (characterObject == currentOrder.objectType)
                {
                    // remove held object from character
                    // move held object to point on truck
                    currentOrder.amount--;
                    if (currentOrder.amount <= 0)
                    {
                        CompleteOrder();
                    }
                }
            }
        }

        public void SetOrder(DayNightManager.DayPhase phase)
        {
            if (phase == newOrderPhase)
            {
                orderCompleted = false;

                currentOrder = new Order();
                int orderIndex = Random.Range(0, possibleOrders.Count);
                currentOrder.objectType = possibleOrders[orderIndex].objectType;
                currentOrder.amount = possibleOrders[orderIndex].amount;

                triggerBox.enabled = true;
                // truck drives in
            }
        }

        public void CompleteOrder()
        {
            orderCompleted = true;
            
            triggerBox.enabled = false;
            // truck drives away
        }
    }
}