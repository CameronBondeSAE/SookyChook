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
            owner.GetComponentInChildren<Wander>().turningAmount = 150;
            tassieModel.atTarget = false;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            foreach (ChickenModel chicken in ChickenManager.Instance.chickensList)
            {
                if (fov.CanISee(chicken.gameObject.transform))
                {
                    tassieModel.seeChicken = true;
                    tassieModel.prey = chicken.transform;
                    tassieModel.isLooking = false;
                    tassieModel.isMoving = true;
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

