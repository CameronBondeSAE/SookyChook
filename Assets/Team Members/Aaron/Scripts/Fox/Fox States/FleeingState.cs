using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Aaron
{
    public class FleeingState : AntAIState
    {
        public GameObject owner;
        
        private bool isDawn;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            
            //Find den point, Get position
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            //Move directly towards den (pahtfinding)
            //Steering behaviour to avoid player sight
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}

