using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Aaron
{
    public class EatChickenState : AntAIState
    {
        public GameObject owner;
        public FoxModel fox;
        public static event Action EatChickenEvent;
        private float hunger;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            fox = GetComponentInParent<FoxModel>();
        }

        public override void Enter()
        {
            base.Enter();

            var foxModel = owner.GetComponent<FoxModel>();
            
            foxModel.hunger += 5;
            if (foxModel.hunger > foxModel.maxHunger)
            {
                foxModel.hunger = foxModel.maxHunger;
            }

            foxModel.chickenGone = true;
            foxModel.isHunting = false;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
        }

        public override void Exit()
        {
            base.Exit();
            
            owner.GetComponent<Wander>().enabled = true;
        }
    }
}
