using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class AttackEnemyState : AntAIState
    {
        public GameObject owner;
    
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            // Used for testing until enemy health is in
            transform.root.Rotate(Vector3.up, 360 * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}