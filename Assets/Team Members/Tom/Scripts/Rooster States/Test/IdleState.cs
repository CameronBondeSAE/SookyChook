using System.Collections;
using System.Collections.Generic;
using Aaron;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class IdleState : AntAIState
    {
        private Rigidbody rb;
        private PathfindingAgent agent;
        private float turnForce = 20f;
        private float speed = 20f;
        private Transform targetChicken;
        private PathfindingGrid.Node targetNode;
        public float followRange = 1.5f;
        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            agent = aGameObject.GetComponent<PathfindingAgent>();
            rb = aGameObject.GetComponent<Rigidbody>();
        }

        public override void Enter()
        {
            base.Enter();

            List<GameObject> chickens = ChickenManager.Instance.chickensList;
            targetChicken = chickens[Random.Range(0, chickens.Count)].transform;

            agent.FindPath(agent.ConvertPositionToNodeCoordinates(transform.position), 
                agent.ConvertPositionToNodeCoordinates(targetChicken.position));
            
            targetNode = agent.path[agent.path.Count - 1];
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
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
        }
    }
}