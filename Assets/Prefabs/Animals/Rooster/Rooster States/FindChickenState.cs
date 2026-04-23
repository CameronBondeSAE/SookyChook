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
        private MoveForward forward;
        private Wander wander;
    
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            rooster = owner.GetComponent<Rooster_Model>();
            forward = owner.GetComponentInChildren<MoveForward>();
            wander = owner.GetComponentInChildren<Wander>();
        }

        public override void Enter()
        {
            base.Enter();

            wander.enabled = true;
            forward.enabled = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (rooster.target == null)
            {
                List<ChickenModel> chickens = ChickenManager.Instance.chickensList;
                if (chickens.Count > 0)
                {
                    rooster.target = chickens[Random.Range(0, chickens.Count)].gameObject.transform;
                    Finish();
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            wander.enabled = false;
            forward.enabled = false;
        }
    }
}