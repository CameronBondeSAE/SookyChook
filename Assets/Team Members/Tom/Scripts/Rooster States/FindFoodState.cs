using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class FindFoodState : AntAIState
    {
        public GameObject owner;
        private Wander wander;
        private MoveForward forward;
        private Vision vision;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            wander = owner.GetComponentInChildren<Wander>();
            vision = owner.GetComponent<Vision>();
            forward = owner.GetComponentInChildren<MoveForward>();
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

            foreach (Edible edible in Edible.edibles)
            {
                if (edible.GetComponent<GrassEdible>())
                {
                    if (vision.CanSeeTarget(edible.transform))
                    {
                        owner.GetComponent<RoosterSense>().food = edible.transform;
                        Finish();
                    }
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