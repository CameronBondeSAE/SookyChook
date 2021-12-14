using System.Collections;
using System.Collections.Generic;
using Aaron;
using Anthill.AI;
using UnityEngine;

namespace Rob
{


    public class FindAChickenState : SookyAntAIState
    {
        private TassieDevilModel tassieModel;
        private FOV fov;
       

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            
            
            fov = owner.GetComponent<FOV>();
            tassieModel = owner.GetComponent<TassieDevilModel>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            foreach (GameObject chicken in ChickenManager.Instance.chickensList)
            {
                if (fov.CanISee(chicken.transform))
                {
                    tassieModel.prey = chicken.transform;
                    tassieModel.isLooking = false;
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
