using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class MoveToChickenState : AntAIState
    {
        public GameObject owner;
        private Rooster_Model rooster;
        private Rigidbody rb;
        private PathfindingAgent agent;
        public float speed = 50f;
        private PathfindingGrid.Node targetNode;
        public float followRange = 1.5f;
        private float timer;
        public float pathfindInterval = 1f;
    
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            rooster = owner.GetComponent<Rooster_Model>();
            rb = owner.GetComponent<Rigidbody>();
            agent = owner.GetComponent<PathfindingAgent>();
        }

        public override void Enter()
        {
            base.Enter();
            
            agent.FindPath(agent.ConvertPositionToNodeCoordinates(transform.position), 
                agent.ConvertPositionToNodeCoordinates(rooster.targetChicken.position));
            
            targetNode = agent.path[agent.path.Count - 1];
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            timer -= aDeltaTime;

            if (timer <= 0)
            {
                agent.FindPath(agent.ConvertPositionToNodeCoordinates(transform.position), 
                    agent.ConvertPositionToNodeCoordinates(rooster.targetChicken.position));
                targetNode = agent.path[agent.path.Count - 1];

                timer = pathfindInterval;
            }
            
            Vector2 position = new Vector2(transform.root.position.x, transform.root.position.z);
            Vector3 targetRotation = new Vector3(targetNode.coordinates.x, transform.root.position.y,
                targetNode.coordinates.y);
            transform.root.LookAt(targetRotation);

            if (Vector2.Distance(targetNode.coordinates, position) < followRange)
            {
                // Finds the next node in the path
                targetNode = agent.path[agent.path.IndexOf(targetNode) - 1];
            }
            
            rb.AddForce(transform.root.forward * speed * Time.deltaTime, ForceMode.VelocityChange);

            if (Vector3.Distance(owner.transform.position, rooster.targetChicken.position) < 1f)
            {
                rooster.targetChicken = null;
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}