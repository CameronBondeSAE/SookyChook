using System.Collections;
using System.Collections.Generic;
using Aaron;
using Anthill.AI;
using UnityEngine;

namespace Rob
{


    public class FindChickenState : AntAIState
    {
        private TassieDevilModel tassieModel;
        public GameObject owner;
        public ChickenManager chickenManager;
        private FOV fov;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            GetComponent<FOV>();
            GetComponent<TassieDevilModel>();

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            foreach (GameObject chicken in chickenManager.chickensList)
            {
                fov.CanISee(chicken.transform);
                if (fov.canSeeTarget)
                {
                    tassieModel.seeChicken = true;
                }
            }

        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}

