using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Tom
{
    public class MoveToTargetState : AntAIState
    {
        public GameObject owner;
        private Rooster_Model rooster;
        private PathfindingAgent agent;
        private TurnToward turn;
        private MoveForward move;
        private PathfindingGrid.Node targetNode;
        public float followRange = 1.5f;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            rooster = owner.GetComponent<Rooster_Model>();
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
                if (nodeIndex < agent.path.Count - 1)
                {
                    targetNode = agent.path[nodeIndex + 1];
                    turn.target = new Vector3(targetNode.coordinates.x, 0, targetNode.coordinates.y);
                }
                else
                {
                    rooster.target = null;
                    Finish();
                }
            }
            
            // Additional check if pathfinding goal is reached but planner isn't updated
            if (rooster.target != null &&
                Vector2.Distance(position, agent.ConvertPositionToNodeCoordinates(rooster.target.position))
                < 1.5f)
            {
                rooster.target = null;
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
            agent.FindPath(transform.position,rooster.target.position);

            targetNode = agent.path[0];
            turn.target = new Vector3(targetNode.coordinates.x, 0, targetNode.coordinates.y);
        }
    }
}