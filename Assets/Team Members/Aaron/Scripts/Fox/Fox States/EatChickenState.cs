using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Aaron
{
    public class EatChickenState : AntAIState
    {
        public GameObject owner;
        public GameObject fox;

        private float hunger;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();

            owner.GetComponent<FoxModel>().hunger += 5;
            if (owner.GetComponent<FoxModel>().hunger > owner.GetComponent<FoxModel>().maxHunger)
            {
                owner.GetComponent<FoxModel>().hunger = owner.GetComponent<FoxModel>().maxHunger;
            }

            owner.GetComponent<FoxModel>().chickenGone = true;
            owner.GetComponent<FoxModel>().isHunting = false;
            
            Debug.Log("Eating State");
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
