using System.Collections;
using System.Collections.Generic;
using Aaron;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class FindChickenState : AntAIState
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
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (rooster.targetChicken == null)
            {
                List<GameObject> chickens = ChickenManager.Instance.chickensList;
                if (chickens.Count > 0)
                {
                    rooster.targetChicken = chickens[Random.Range(0, chickens.Count)].transform;
                    Finish();
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}