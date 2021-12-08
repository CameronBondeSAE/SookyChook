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
        public GameObject chickenTarget;
        FoxModel foxModel;
            
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            
            FoxModel foxModel = owner.GetComponent<FoxModel>();
            
            chickenTarget = foxModel.target;

            if (foxModel.inRange == true)
            {
                if(foxModel.target.GetComponent<Health>())
                Attack();
            }
            else
            {
                foxModel.inRange = false;
            }

            //owner.GetComponent<FoxModel>().eatingChicken = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
        }

        public override void Exit()
        {
            base.Exit();
            
            
        }

        void Attack()
        {
            foxModel.target.GetComponent<Health>().ChangeHealth(-5);
        }

    }
}
