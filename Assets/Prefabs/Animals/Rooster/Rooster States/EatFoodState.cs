using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class EatFoodState : AntAIState
    {
        public GameObject owner;
        private Rooster_Model rooster;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            rooster = owner.GetComponent<Rooster_Model>();
        }

        public override void Enter()
        {
            base.Enter();

            StartCoroutine(EatFood());
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public IEnumerator EatFood()
        {
            rooster.target.GetComponent<Edible>().BeingEaten(0.5f);
            yield return new WaitForSeconds(1f);
            rooster.ChangeHunger(-0.5f);
            rooster.target.GetComponent<Edible>().BeingEaten(0.5f);
        }
    }
}