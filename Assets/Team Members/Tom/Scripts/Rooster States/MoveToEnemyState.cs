using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class MoveToEnemyState : AntAIState
    {
        public GameObject owner;
        private Rigidbody rb;
        private TurnToward turn;
        private Rooster_Model rooster;
        private PathfindingAgent agent;
        private FollowPath follow;
        public float moveSpeed = 20f;
        public float pathfindInterval = 1f;
        private float timer;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            rb = owner.GetComponent<Rigidbody>();
            turn = owner.GetComponent<TurnToward>();
            follow = owner.GetComponent<FollowPath>();
            agent = owner.GetComponent<PathfindingAgent>();
            rooster = owner.GetComponent<Rooster_Model>();
        }

        public override void Enter()
        {
            base.Enter();

            List<TassieDevilModel> enemies = rooster.FindObjects<TassieDevilModel>(rooster.sightRange);

            // Find closest enemy
            float closestDistance = Mathf.Infinity;
            foreach (TassieDevilModel enemy in enemies)
            {
                float distance = Vector3.Distance(transform.root.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    rooster.targetEnemy = enemy.transform;
                }
            }
            
            agent.FindPath(agent.ConvertPositionToNodeCoordinates(transform.position), 
                agent.ConvertPositionToNodeCoordinates(rooster.targetEnemy.position));
            
            turn.enabled = true;
            follow.enabled = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            timer -= aDeltaTime;

            if (timer <= 0)
            {
                agent.FindPath(agent.ConvertPositionToNodeCoordinates(transform.position), 
                    agent.ConvertPositionToNodeCoordinates(rooster.targetEnemy.position));

                timer = pathfindInterval;
            }
        }

        public override void Exit()
        {
            base.Exit();

            turn.enabled = false;
            follow.enabled = false;
        }
    }
}