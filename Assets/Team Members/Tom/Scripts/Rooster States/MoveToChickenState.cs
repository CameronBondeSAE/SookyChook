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
        private TurnToward turn;
        private MoveForward move;
        public float speed = 50f;
        private PathfindingGrid.Node targetNode;
        public float followRange = 1.5f;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            rooster = owner.GetComponent<Rooster_Model>();
            rb = owner.GetComponent<Rigidbody>();
            agent = owner.GetComponent<PathfindingAgent>();
            turn = owner.GetComponentInChildren<TurnToward>();
            move = owner.GetComponentInChildren<MoveForward>();

            GlobalEvents.levelStaticsUpdated += FindNewPath;
        }

        public override void Enter()
        {
            base.Enter();

            turn.enabled = true;
            move.enabled = true;
            FindNewPath(gameObject);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            Vector2 position = new Vector2(transform.root.position.x, transform.root.position.z);

            if (Vector2.Distance(targetNode.coordinates, position) < followRange)
            {
                // Finds the next node in the path
                int nodeIndex = agent.path.IndexOf(targetNode);
                if (nodeIndex > 0)
                {
                    targetNode = agent.path[nodeIndex - 1];
                    turn.target = new Vector3(targetNode.coordinates.x, 0, targetNode.coordinates.y);
                }
                else
                {
                    rooster.targetChicken = null;
                    Finish();
                }
            }

            if (rooster.targetChicken != null &&
                Vector2.Distance(position, agent.ConvertPositionToNodeCoordinates(rooster.targetChicken.position))
                < 1.5f)
            {
                rooster.targetChicken = null;
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();

            turn.enabled = false;
            move.enabled = false;
        }

        public void FindNewPath(GameObject go)
        {
            agent.FindPath(agent.ConvertPositionToNodeCoordinates(transform.position),
                agent.ConvertPositionToNodeCoordinates(rooster.targetChicken.position));

            targetNode = agent.path[agent.path.Count - 1];
            turn.target = new Vector3(targetNode.coordinates.x, 0, targetNode.coordinates.y);
        }
    }
}