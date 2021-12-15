using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rob
{
    public class FollowPath : MonoBehaviour
    {
        //public PathFinding pathFinder;
        public float distanceToNode;
        public int currentIndex;
        public TassieDevilModel tassieDevilModel;
        public float turningSpeed;

        public float newSpeed;
        public float oldSpeed;

        public Wander wander;
        public Forward forward;
        
        

        List<WorldScan.Node> path;

        private WorldScan.Node targetPathNode;
        private Rigidbody rb;


        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            oldSpeed = forward.speed;
        }


        public void SetPath(Transform target)
        {
            PathFinding.Instance.startPos = PathFinding.Instance.ConvertWorldToGridSpace(transform.position);
            PathFinding.Instance.endPos = PathFinding.Instance.ConvertWorldToGridSpace(target.position);
            path = PathFinding.Instance.FindPath().ToList();
            currentIndex = 0;
            targetPathNode = path[0];
        }

        public void TakePath()
        {
            if (PathFinding.Instance.path.Count > 0 && !tassieDevilModel.atTarget)
            {
                wander.enabled = false;
                forward.speed = newSpeed;
                Vector3 targetNodeWorld = PathFinding.Instance.ConvertGridToWorldSpace(targetPathNode.gridPos);
                Vector3 targetWorldPosition = transform.InverseTransformPoint(targetNodeWorld);
                float turningDirection = targetWorldPosition.x;
                rb.AddRelativeTorque(Vector3.up * turningSpeed * turningDirection, ForceMode.Acceleration);
                if (Vector3.Distance(transform.position, targetNodeWorld) < distanceToNode)
                {
                    currentIndex++;

                    if (currentIndex >= path.Count - 1)
                    {
                        currentIndex = path.Count - 1;
                        tassieDevilModel.atTarget = true;
                        wander.enabled = true;
                        forward.speed = oldSpeed;
                    }

                    targetPathNode = path[currentIndex];
                }
            }
        }

        public void PathToChicken(Transform aTransform)
        {
            
        }
    }
}