using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class FollowPath : MonoBehaviour
    {
        private TurnToward turn;
        public PathfindingAgent pathfinding;
        private PathfindingGrid.Node targetNode;
        public float followRange = 1f;

        public void Start()
        {
            turn = GetComponent<TurnToward>();
            targetNode = pathfinding.path[pathfinding.path.Count - 1];
            turn.target = new Vector3(targetNode.coordinates.x, 0, targetNode.coordinates.y);
        }

        public void Update()
        {
            if (pathfinding.path != null)
            {
                Vector2 position = new Vector2(transform.position.x, transform.position.z);

                if (Vector2.Distance(targetNode.coordinates, position) < followRange)
                {
                    // Finds the next node in the path
                    targetNode = pathfinding.path[pathfinding.path.IndexOf(targetNode) - 1];
                    turn.target = new Vector3(targetNode.coordinates.x, 0, targetNode.coordinates.y);
                }
            }
        }
    }
}