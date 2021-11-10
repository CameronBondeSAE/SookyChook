using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Aaron
{
    public class MoveToChickenState : AntAIState
    {
        public GameObject owner;

        private bool inRangeOfChicken;
        private bool canSeeChicken;
        private bool isVisibleToPlayer;
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            
            //Find chicken, get position

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            //Steering behaviour to avoid player's sight
            //Move towards chicken
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}

