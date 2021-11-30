using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using UnityEngine;

namespace Aaron
{
    public class MoveToHidingSpotState : AntAIState
    {
        public GameObject owner;

        private bool isInHidingSpot;
        private bool canSeeHidingSpot;
        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Moving to Hide State");
            //Find hiding spot, Get position
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            //Move directly towards hiding spot (pathfinding)
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
