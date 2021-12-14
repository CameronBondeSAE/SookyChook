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

        private bool atCarcass = false;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            fox = GetComponentInParent<FoxModel>();
        }

        public override void Enter()
        {
            base.Enter();

            if (atCarcass)
            {
                StartCoroutine(EatingChicken());
            }

            fox.hunger += 5;
            if (fox.hunger > fox.maxHunger)
            {
                fox.hunger = fox.maxHunger;
            }

            fox.chickenGone = true;
            fox.isHunting = false;
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

        IEnumerator EatingChicken()
        {
            EatChickenEvent?.Invoke();
            yield return new WaitForSeconds(5);
        }
    }
}
