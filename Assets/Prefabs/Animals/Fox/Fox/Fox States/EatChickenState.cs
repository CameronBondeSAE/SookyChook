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
        private float distance;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            owner = aGameObject;
            fox = GetComponentInParent<FoxModel>();
        }

        public override void Enter()
        {
            base.Enter();

            fox.chickenGone = true;
            fox.isHunting = false;
            fox.inRange = true;
            fox.GetComponent<Wander>().enabled = false;
            fox.GetComponent<MoveForward>().enabled = false;
            
            StartCoroutine(EatingChicken());
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
            
            //TODO after Cam pushes Edible update
            //Edible.BeingEaten();
            
            fox.hunger += 5;
            if (fox.hunger > fox.maxHunger)
            {
                fox.hunger = fox.maxHunger;
            }
            
            yield return new WaitForSeconds(5);
            
            Finish();
        }
    }
}
