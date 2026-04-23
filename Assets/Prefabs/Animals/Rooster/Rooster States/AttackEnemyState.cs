using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class AttackEnemyState : AntAIState
    {
        public GameObject owner;
        private float attackTimer;
        public float attackTime = 1f;
        public float damage = 5f;
        
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
            attackTimer -= aDeltaTime;

            if (attackTimer <= 0)
            {
                owner.GetComponent<Rooster_Model>().target.GetComponent<Health>().ChangeHealth(-damage);
                attackTimer = attackTime;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}