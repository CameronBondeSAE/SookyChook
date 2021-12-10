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
        List<WorldScan.Node> path;
        private WorldScan.Node targetPathNode;
        public TassieDevilModel tassieDevilModel;


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
            if (PathFinding.Instance.path.Count > 0)
            {
                //HACK change later
                Vector3 targetNodeWorld = PathFinding.Instance.ConvertGridToWorldSpace(targetPathNode.gridPos);
                transform.LookAt(targetNodeWorld);
                transform.Translate(0, 0, Time.deltaTime * 5, Space.Self);
                if (Vector3.Distance(transform.position, targetNodeWorld) < distanceToNode)
                {
                    currentIndex++;
                    
                    if (currentIndex >= path.Count - 1)
                    {
                        tassieDevilModel.atPrey = true;
                    }
                    
                    targetPathNode = path[currentIndex];
                }
            }

            
        }
    }
}