using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;


namespace Rob
{

    public class Idle : SookyAntAIState
    {
        private TassieDevilModel tassieDevilModel;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            tassieDevilModel = owner.GetComponent<TassieDevilModel>();

        }

        public override void Enter()
        {
            base.Enter();
            owner.GetComponentInChildren<Forward>().speed = 20;
            owner.GetComponentInChildren<Wander>().enabled = true;

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (tassieDevilModel.isHungry)
            {
                Finish();
            }
            
        }

        public override void Exit()
        {
            base.Exit();
        }

       
    }
}
