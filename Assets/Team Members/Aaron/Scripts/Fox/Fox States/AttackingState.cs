using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Aaron
{
    public class AttackingState : AntAIState
    {
        private event Action EatingChicken ;
        public GameObject owner;
        public GameObject chickenTarget;
            
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();

            chickenTarget = owner.GetComponent<FoxModel>().target;

            if (owner.GetComponent<FoxModel>().inRange == true)
            {
                EatingChicken?.Invoke();
            }

            owner.GetComponent<FoxModel>().eatingChicken = true;

            Debug.Log("Attacking State");
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
        }

        public override void Exit()
        {
            base.Exit();
            
            
        }

    }
}
