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
