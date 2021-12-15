using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEditor;
using UnityEngine;

namespace Aaron
{
    public class AttackingState : AntAIState
    {
        private event Action EatingChicken ;
        
        public GameObject owner;
        public Edible chickenTarget;
        
        FoxModel foxModel;

        private bool hasAttacked = false;
        public bool attackRange = false;
            
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            foxModel = owner.GetComponent<FoxModel>();
            ChickenManager.Instance.ChickenDeathEvent += EatChicken;
        }

        public override void Enter()
        {
            base.Enter();
            
            owner.GetComponent<FoxModel>();
            
            chickenTarget = foxModel.target;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            float distance = Vector3.Distance(chickenTarget.transform.position, owner.transform.position);
            
            
            if (distance <= 1f)
            {
                attackRange = true;
                foxModel.GetComponent<MoveForward>().enabled = false;
            }
            if (distance > 1)
            {
                attackRange = false;
                foxModel.GetComponent<FoxModel>().inRange = false;
                //owner.GetComponent<Wander>().enabled = true;
                foxModel.GetComponent<TurnTowards>().enabled = true;
                foxModel.GetComponent<MoveForward>().enabled = true;
            }
            
            if (attackRange)
            {
                if(hasAttacked == false)
                {
                    if (chickenTarget.GetComponent<Health>())
                    {
                        Attack();
                        hasAttacked = true;
                        StartCoroutine(AttackCooldown());
                    }
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        void Attack()
        {
            chickenTarget.GetComponent<Health>().ChangeHealth(-5);
        }

        void EatChicken()
        {
            owner.GetComponent<FoxModel>().eatingChicken = true;
        }

        private IEnumerator AttackCooldown()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1);
            }

            hasAttacked = false;
        }

    }
}
